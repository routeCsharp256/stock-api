using System.Threading;
using System.Threading.Tasks;
using Npgsql;
using OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate;
using OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.StockApi.Infrastructure.Repositories.Implementation
{
    public class DeliveryRequestRepository : IDeliveryRequestRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IEntitiesHolder _entitiesHolder;

        public DeliveryRequestRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IEntitiesHolder entitiesHolder)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _entitiesHolder = entitiesHolder;
        }
        
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