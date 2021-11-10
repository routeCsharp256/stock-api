using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Npgsql;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Domain.Contracts;

namespace OzonEdu.StockApi.Infrastructure.Repositories
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IMediator _mediator;
        private readonly IRepositoriesHolder _repositoriesHolder;
        private readonly IEntitiesHolder _entitiesHolder;

        public UnitOfWorkFactory(
            IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IMediator mediator,
            IRepositoriesHolder repositoriesHolder,
            IEntitiesHolder entitiesHolder)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _mediator = mediator;
            _entitiesHolder = entitiesHolder;
            _repositoriesHolder = repositoriesHolder;
        }

        public async Task<IAggregateUnitOfWork> Create(CancellationToken token)
        {
            await _dbConnectionFactory.CreateConnection();
            var uow = new UnitOfWork(_dbConnectionFactory, _mediator, _repositoriesHolder, _entitiesHolder);
            await uow.CreateTransaction(token);
            return uow;
        }
    }
}