using System.Threading;
using System.Threading.Tasks;
using OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate;

namespace OzonEdu.StockApi.Infrastructure.Repositories.Implementation
{
    public class DeliveryRequestRepository : IDeliveryRequestRepository
    {
        public Task<DeliveryRequest> CreateAsync(DeliveryRequest itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<DeliveryRequest> UpdateAsync(DeliveryRequest itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<DeliveryRequest> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<DeliveryRequest> FindByRequestNumberAsync(RequestNumber requestNumber, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}