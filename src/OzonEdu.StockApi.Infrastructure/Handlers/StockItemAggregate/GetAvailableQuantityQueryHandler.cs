using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.StockApi.Infrastructure.Models;
using OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate;
using OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate.Responses;
using OzonEdu.StockApi.Infrastructure.Repositories.Models;
using Sku = OzonEdu.StockApi.Domain.AggregationModels.ValueObjects.Sku;

namespace OzonEdu.StockApi.Infrastructure.Handlers.StockItemAggregate
{
    public class GetAvailableQuantityQueryHandler : IRequestHandler<GetStockItemsAvailableQuantityQuery, GetStockItemsAvailableQuantityQueryResponse>
    {
        private readonly IStockItemRepository _stockItemRepository;

        public GetAvailableQuantityQueryHandler(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        public async Task<GetStockItemsAvailableQuantityQueryResponse> Handle(GetStockItemsAvailableQuantityQuery request, CancellationToken cancellationToken)
        {
            var result = await _stockItemRepository.FindBySkusAsync(request.Skus.Select(x => 
                new OzonEdu.StockApi.Domain.AggregationModels.ValueObjects.Sku(x)).ToList(), cancellationToken);
            return new GetStockItemsAvailableQuantityQueryResponse
            {
                Items = result.Select(x => new StockItemQuantityDto
                {
                    Sku = x.Sku.Value,
                    Quantity = x.Quantity.Value
                }).ToList()
            };
        }
    }
}