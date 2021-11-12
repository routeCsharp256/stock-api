using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate;
using OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate.Responses;

namespace OzonEdu.StockApi.Infrastructure.Handlers.StockItemAggregate
{
    public class GetStockItemsAvailableQuantityQueryHandler : IRequestHandler<GetStockItemsAvailableQuantityQuery, GetStockItemsAvailableQuantityQueryResponse>
    {
        private readonly IStockItemRepository _stockItemRepository;

        public GetStockItemsAvailableQuantityQueryHandler(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        public Task<GetStockItemsAvailableQuantityQueryResponse> Handle(GetStockItemsAvailableQuantityQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}