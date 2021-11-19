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
        private readonly IQueryExecutor _queryExecutor;

        public DeliveryRequestRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IQueryExecutor queryExecutor)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _queryExecutor = queryExecutor;
        }
        
        public Task<DeliveryRequest> CreateAsync(DeliveryRequest itemToCreate, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<DeliveryRequest> FindByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<DeliveryRequest> FindByRequestNumberAsync(RequestNumber requestNumber, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}