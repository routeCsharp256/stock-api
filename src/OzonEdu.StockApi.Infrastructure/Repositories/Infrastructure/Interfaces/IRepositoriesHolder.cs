using OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;

namespace OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure.Interfaces
{
    /// <summary>
    /// Компонент, содержащий в себе все используемые в приложении репозитории.
    /// </summary>
    ///  <remarks>
    /// Необходим для того, чтобы при появления нового репозитория
    /// нам не пришлось изменять код UnitOfWorkFactory.
    /// </remarks>
    public interface IRepositoriesHolder
    {
        public IDeliveryRequestRepository DeliveryRequestRepository { get; }

        public IStockItemRepository StockItemRepository { get; }
    }
}