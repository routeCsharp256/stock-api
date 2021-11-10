using OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure
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