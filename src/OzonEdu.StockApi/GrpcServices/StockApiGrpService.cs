using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using OzonEdu.StockApi.Grpc;
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
            Empty _,
            ServerCallContext context)
        {
            // Использовать mediatr
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

        public override Task<Empty> AddStockItem(AddStockItemRequest request, ServerCallContext context)
        {
            // Использовать mediatr
            throw new NotImplementedException();
        }
    }
}