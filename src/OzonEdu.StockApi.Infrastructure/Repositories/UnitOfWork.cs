using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Npgsql;
using OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Domain.Contracts;

namespace OzonEdu.StockApi.Infrastructure.Repositories
{
    public class UnitOfWork : IAggregateUnitOfWork
    {
        private NpgsqlTransaction _npgsqlTransaction;
        
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory = null;
        private readonly IMediator _mediator;
        private readonly IRepositoriesHolder _repositoriesHolder;
        private readonly IEntitiesHolder _entitiesHolder;

        public UnitOfWork(
            IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IMediator mediator,
            IRepositoriesHolder repositoriesHolder,
            IEntitiesHolder entitiesHolder)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _mediator = mediator;
            _repositoriesHolder = repositoriesHolder;
            _entitiesHolder = entitiesHolder;
        }

        public IDeliveryRequestRepository DeliveryRequestRepository => _repositoriesHolder.DeliveryRequestRepository;

        public IStockItemRepository StockItemRepository => _repositoriesHolder.StockItemRepository;

        public async ValueTask CreateTransaction(CancellationToken token)
        {
            if (_npgsqlTransaction is not null)
            {
                return;
            }

            _npgsqlTransaction = await _dbConnectionFactory.Connection.BeginTransactionAsync(token);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            var domainEvents = new Queue<INotification>(
                _entitiesHolder.UsedEntities
                    .SelectMany(x => x.DomainEvents));
            // Можно отправлять все и сразу через Task.WhenAll.
            while (domainEvents.TryDequeue(out var notification))
            {
                await _mediator.Publish(notification, cancellationToken);
            }

            await _npgsqlTransaction.CommitAsync(cancellationToken);
        }

        void IDisposable.Dispose()
        {
            _npgsqlTransaction?.Dispose();
            _dbConnectionFactory?.Dispose();
        }
    }
}