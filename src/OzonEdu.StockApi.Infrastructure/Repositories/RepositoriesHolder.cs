using OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;

namespace OzonEdu.StockApi.Infrastructure.Repositories
{
    public class RepositoriesHolder : IRepositoriesHolder
    {
        public RepositoriesHolder(IStockItemRepository stockItemRepository, IDeliveryRequestRepository deliveryRequestRepository)
        {
            StockItemRepository = stockItemRepository;
            DeliveryRequestRepository = deliveryRequestRepository;
        }

        public IDeliveryRequestRepository DeliveryRequestRepository { get; }
        public IStockItemRepository StockItemRepository { get; }
    }
}