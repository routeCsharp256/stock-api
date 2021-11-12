using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Infrastructure.Queries;
using OzonEdu.StockApi.Infrastructure.Queries.Responses;

namespace OzonEdu.StockApi.Infrastructure.Handlers
{
    public class GetAllStockItemsQueryHandler : IRequestHandler<GetAllStockItemsQuery, GetAllStockItemsQueryResponse>
    {
        private readonly IStockItemRepository _stockItemRepository;

        public GetAllStockItemsQueryHandler(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }
        
        public Task<GetAllStockItemsQueryResponse> Handle(GetAllStockItemsQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}