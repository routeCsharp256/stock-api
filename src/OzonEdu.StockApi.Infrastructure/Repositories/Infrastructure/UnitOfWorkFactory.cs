using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Npgsql;
using OzonEdu.StockApi.Domain.Contracts;
using OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure
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

        public async Task<IUnitOfWork> Create(CancellationToken token)
        {
            var uow = new UnitOfWork(_dbConnectionFactory, _mediator, _repositoriesHolder, _entitiesHolder);
            await uow.CreateDbConnection(token);
            await uow.CreateTransaction(token);
            return uow;
        }
    }
}