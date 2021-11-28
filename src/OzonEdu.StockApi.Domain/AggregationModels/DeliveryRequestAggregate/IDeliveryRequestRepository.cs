using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.StockApi.Domain.Contracts;

namespace OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate
{
    /// <summary>
    /// Репозиторий для управления сущностью <see cref="DeliveryRequest"/>
    /// </summary>
    public interface IDeliveryRequestRepository : IRepository<DeliveryRequest>
    {
        Task<DeliveryRequest> CreateAsync(DeliveryRequest itemToCreate, CancellationToken cancellationToken);

        Task<DeliveryRequest> FindByIdAsync(int id, CancellationToken cancellationToken);

        Task<DeliveryRequest> FindByRequestNumberAsync(
            RequestNumber requestNumber,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<DeliveryRequest>> GetRequestsByStatusAsync(RequestStatus requestStatus,
            CancellationToken cancellationToken);
    }
}