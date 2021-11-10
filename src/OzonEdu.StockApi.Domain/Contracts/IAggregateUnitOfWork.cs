using OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;

namespace OzonEdu.StockApi.Domain.Contracts
{
    public interface IAggregateUnitOfWork : IUnitOfWork
    {
        IDeliveryRequestRepository DeliveryRequestRepository { get; }
        IStockItemRepository StockItemRepository { get; }
    }
}