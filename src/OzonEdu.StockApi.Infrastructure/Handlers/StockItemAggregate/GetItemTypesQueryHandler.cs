using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate;
using OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate.Responses;

namespace OzonEdu.StockApi.Infrastructure.Handlers.StockItemAggregate
{
    public class GetItemTypesQueryHandler : IRequestHandler<GetItemTypesQuery, GetItemTypesQueryResponse>
    {
        private readonly IStockItemRepository _stockItemRepository;

        public GetItemTypesQueryHandler(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        public Task<GetItemTypesQueryResponse> Handle(GetItemTypesQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}