using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using OzonEdu.StockApi.Grpc;
using OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate;

namespace OzonEdu.StockApi.GrpcServices
{
    public class StockApiGrpService : StockApiGrpc.StockApiGrpcBase
    {
        private readonly IMediator _mediator;

        public StockApiGrpService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<GetAllStockItemsResponse> GetAllStockItems(
            Empty _,
            ServerCallContext context)
        {
            var result = await _mediator.Send(new GetAllStockItemsQuery(), context.CancellationToken);
            return new GetAllStockItemsResponse()
            {
                Stocks =
                {
                    result.Items.Select(
                            it => new GetAllStockItemsResponseUnit
                            {
                                Quantity = it.Quantity,
                                Sku = it.Sku,
                                ItemTypeId = it.ItemTypeId,
                                ItemName = it.Name,
                                Id = it.Id
                            })
                        .ToList()
                }
            };
        }

        public override Task<ItemTypesResult> GetItemTypes(Empty request, ServerCallContext context)
        {
            return base.GetItemTypes(request, context);
        }

        public override Task<Empty> GiveOutItems(GiveOutItemsRequest request, ServerCallContext context)
        {
            return base.GiveOutItems(request, context);
        }

        public override Task<StockItemsAvailabilityResponse> GetByItemType(IntIdModel request, ServerCallContext context)
        {
            return base.GetByItemType(request, context);
        }

        public override Task<StockItemsAvailabilityResponse> GetStockItemsAvailability(SkusRequest request, ServerCallContext context)
        {
            return base.GetStockItemsAvailability(request, context);
        }
    }
}