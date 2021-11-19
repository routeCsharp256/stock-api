using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Infrastructure.Models;
using OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate;
using OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate.Responses;

namespace OzonEdu.StockApi.Infrastructure.Handlers.StockItemAggregate
{
    public class GetByItemTypeQueryHandler : IRequestHandler<GetByItemTypeQuery, GetByItemTypeQueryResponse>
    {
        private readonly IStockItemRepository _stockItemRepository;

        public GetByItemTypeQueryHandler(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        public async Task<GetByItemTypeQueryResponse> Handle(GetByItemTypeQuery request, CancellationToken 
        cancellationToken)
        {
            var items = await _stockItemRepository.FindByItemTypeAsync(request.Id, cancellationToken);
            return new GetByItemTypeQueryResponse
            {
                Items = items.Select(x => new StockItemDto
                {
                    Sku = x.Sku.Value,
                    Name = x.ItemType.Type.Name,
                    ClothingSizeId = x.ClothingSize?.Id,
                    ItemTypeId = x.ItemType.Id,
                    Quantity = x.Quantity.Value,
                    MinimalQuantity = x.MinimalQuantity.Value ?? 0
                }).ToList()
            };
        }
    }
}