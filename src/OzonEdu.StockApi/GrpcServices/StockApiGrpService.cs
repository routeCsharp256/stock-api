using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using OzonEdu.StockApi.Controllers.V1;
using OzonEdu.StockApi.Grpc;
using OzonEdu.StockApi.Models;
using OzonEdu.StockApi.Services.Interfaces;

namespace OzonEdu.StockApi.GrpcServices
{
    public class StockApiGrpService : StockApiGrpc.StockApiGrpcBase
    {
        private readonly IStockService _stockService;

        public StockApiGrpService(IStockService stockService)
        {
            _stockService = stockService;
        }

        public override async Task<GetAllStockItemsResponse> GetAllStockItems(
            GetAllStockItemsRequest request,
            ServerCallContext context)
        {
            var stockItems = await _stockService.GetAll(context.CancellationToken);
            return new GetAllStockItemsResponse
            {
                Stocks = { stockItems.Select(x => new GetAllStockItemsResponseUnit
                {
                    ItemId = x.ItemId,
                    Quantity = x.Quantity,
                    ItemName = x.ItemName
                })}
            };
        }

        public override async Task<GetAllStockItemsWithNullsResponse> GetAllStockItemsWithNulls(Empty request, ServerCallContext context)
        {
            var stockItems = await _stockService.GetAll(context.CancellationToken);
            return new GetAllStockItemsWithNullsResponse
            {
                Stocks = { stockItems.Select(x => new GetAllStockItemsWithNullsResponseUnit
                {
                    ItemId = x.ItemId,
                    Quantity = x.Quantity,
                    ItemName = x.ItemName
                })}
            };
        }

        public override async Task<GetAllStockItemsMapResponse> GetAllStockItemsMap(Empty request, ServerCallContext context)
        {
            var stockItems = await _stockService.GetAll(context.CancellationToken);
            return new GetAllStockItemsMapResponse
            {
                Stocks =
                {
                    stockItems.ToDictionary(x => x.ItemId, x => new GetAllStockItemsResponseUnit
                    {
                        ItemId = x.ItemId,
                        Quantity = x.Quantity,
                        ItemName = x.ItemName
                    })
                }
            };
        }

        public override Task<Empty> AddStockItem(AddStockItemRequest request, ServerCallContext context)
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, "validation failed"),
                new Metadata {new Metadata.Entry("key", "our value")});
        }

        public override async Task GetAllStockItemsStreaming(
            GetAllStockItemsRequest request,
            IServerStreamWriter<GetAllStockItemsResponseUnit> responseStream,
            ServerCallContext context)
        {
            var stockItems = await _stockService.GetAll(context.CancellationToken);
            foreach (var item in stockItems)
            {
                if (context.CancellationToken.IsCancellationRequested)
                {
                    break;
                }

                await responseStream.WriteAsync(new GetAllStockItemsResponseUnit
                {
                    ItemId = item.ItemId,
                    Quantity = item.Quantity,
                    ItemName = item.ItemName
                });
            }
        }

        public override async Task<Empty> AddStockItemStreaming(IAsyncStreamReader<AddStockItemRequest> requestStream,
            ServerCallContext context)
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await requestStream.MoveNext();
                var currentItem = requestStream.Current;

                await _stockService.Add(new StockItemCreationModel
                {
                    Quantity = currentItem.Quantity,
                    ItemName = currentItem.ItemName
                }, context.CancellationToken);
            }

            return new Empty();
        }
    }
}