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
using OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure.Exceptions;
using OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure
{
    public class UnitOfWork : IAggregateUnitOfWork
    {
        private NpgsqlTransaction _npgsqlTransaction;
        
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory = null;
        private readonly IPublisher _publisher;
        private readonly IRepositoriesHolder _repositoriesHolder;
        private readonly IEntitiesHolder _entitiesHolder;

        public UnitOfWork(
            IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IPublisher publisher,
            IRepositoriesHolder repositoriesHolder,
            IEntitiesHolder entitiesHolder)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _publisher = publisher;
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
            if (_npgsqlTransaction is null)
            {
                throw new NoActiveTransactionStartedException();
            }

            var domainEvents = new Queue<INotification>(
                _entitiesHolder.UsedEntities
                    .SelectMany(x => x.DomainEvents));
            // Можно отправлять все и сразу через Task.WhenAll.
            while (domainEvents.TryDequeue(out var notification))
            {
                await _publisher.Publish(notification, cancellationToken);
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