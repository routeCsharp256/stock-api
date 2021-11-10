using OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;

namespace OzonEdu.StockApi.Infrastructure.Repositories
{
    // Компонент, необходимый для того, чтобы при увеличении количества репозиториев
    // нам не пришлось изменять код UnitOfWorkFactory.
    public interface IRepositoriesHolder
    {
        public IDeliveryRequestRepository DeliveryRequestRepository { get; }

        public IStockItemRepository StockItemRepository { get; }
    }
}