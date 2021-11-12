using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Infrastructure.Queries;
using OzonEdu.StockApi.Infrastructure.Queries.Responses;

namespace OzonEdu.StockApi.Infrastructure.Handlers
{
    public class GetByItemTypeQueryHandler : IRequestHandler<GetByItemTypeQuery, GetByItemTypeQueryResponse>
    {
        private readonly IStockItemRepository _stockItemRepository;

        public GetByItemTypeQueryHandler(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        public Task<GetByItemTypeQueryResponse> Handle(GetByItemTypeQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}