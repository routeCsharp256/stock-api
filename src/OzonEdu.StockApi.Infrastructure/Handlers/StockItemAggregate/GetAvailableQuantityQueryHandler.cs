using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Infrastructure.Queries;

namespace OzonEdu.StockApi.Infrastructure.Handlers
{
    public class GetAvailableQuantityQueryHandler : IRequestHandler<GetAvailableQuantityQuery, GetAvailableQuantityQueryResponse>
    {
        private readonly IStockItemRepository _stockItemRepository;

        public GetAvailableQuantityQueryHandler(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        public Task<GetAvailableQuantityQueryResponse> Handle(GetAvailableQuantityQuery request, CancellationToken cancellationToken)
        {
            // Поиск по всем элементам и получение количества
            return Task.FromResult(new GetAvailableQuantityQueryResponse());
        }
    }
}