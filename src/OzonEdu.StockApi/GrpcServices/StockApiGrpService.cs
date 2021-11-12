using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using OzonEdu.StockApi.Grpc;
using OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate;
using OzonEdu.StockApi.Infrastructure.Commands;
using OzonEdu.StockApi.Infrastructure.Commands.GiveOutStockItem;
using OzonEdu.StockApi.Infrastructure.Models;
using OzonEdu.StockApi.Infrastructure.Queries;

namespace OzonEdu.StockApi.GrpcServices
{
    public class StockApiGrpService : StockApiGrpc.StockApiGrpcBase
    {
        private readonly IMediator _mediator;

        public StockApiGrpService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<StockItemsResponse> GetAllStockItems(
            Empty _,
            ServerCallContext context)
        {
            var response = await _mediator.Send(new GetAllStockItemsQuery(), context.CancellationToken);
            return new StockItemsResponse
            {
                Items =
                {
                    response.Items.Select(
                        it => new StockItemUnit
                        {
                            Id = it.Id,
                            Quantity = it.Quantity,
                            Sku = it.Sku,
                            ItemName = it.Name,
                            ItemTypeId = it.ItemTypeId
                        })
                }
            };
        }

        public override async Task<ItemTypesResult> GetItemTypes(Empty request, ServerCallContext context)
        {
            var response = await _mediator.Send(new GetItemTypesQuery(), context.CancellationToken);
            var result = new ItemTypesResult();
            result.Items.AddRange(response.Items.Select(it => new ItemTypeModel
            {
                Id = it.Id,
                Name = it.Name
            }));
            return result;
        }

        public override async Task<Empty> GiveOutItems(GiveOutItemsRequest request, ServerCallContext context)
        {
            var response = await _mediator.Send(new GiveOutStockItemCommand()
            {
                Items = request.Items.Select(it => new StockItemQuantityDto()
                {
                    Quantity = it.Quantity,
                    Sku = it.Sku
                }).ToList()
            }, context.CancellationToken);
            return new Empty();
        }

        public override async Task<StockItemsResponse> GetByItemType(IntIdModel request, ServerCallContext context)
        {
            var response = await _mediator.Send(new GetByItemTypeQuery()
            {
                Id = request.Id
            }, context.CancellationToken);
            var result = new StockItemsResponse();
            result.Items.AddRange(response.Items.Select(it => new StockItemUnit()
                {
                    Id = it.Id,
                    Quantity = it.Quantity,
                    Sku = it.Sku,
                    ItemName = it.Name,
                    ItemTypeId = it.ItemTypeId
                }
            ).ToList());
            return result;
        }

        public override async Task<StockItemsAvailabilityResponse> GetStockItemsAvailability(SkusRequest request, ServerCallContext context)
        {
            var response = await _mediator.Send(new GetStockItemsAvailableQuantityQuery()
            {
                Skus = request.Skus
            }, context.CancellationToken);
            var result = new StockItemsAvailabilityResponse();
            result.Items.AddRange(response.Items.Select(it => new SkuQuantityItem()
                {
                    Quantity = it.Quantity,
                    Sku = it.Sku,
                }
            ).ToList());
            return result;
        }
    }
}