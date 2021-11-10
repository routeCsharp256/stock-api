using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;

namespace OzonEdu.StockApi.Infrastructure.Repositories
{
    public class StockItemRepository : IStockItemRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IEntitiesHolder _entitiesHolder;

        public StockItemRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IEntitiesHolder entitiesHolder)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _entitiesHolder = entitiesHolder;
        }
        public Task<StockItem> CreateAsync(StockItem itemToCreate, CancellationToken cancellationToken = default)
        {
            // Добавление после успешно выполненной операции.
            _entitiesHolder.Hold(itemToCreate);
            return Task.FromResult(itemToCreate);
        }

        public Task<StockItem> UpdateAsync(StockItem itemToUpdate, CancellationToken cancellationToken = default)
        {
            // Добавление после успешно выполненной операции.
            _entitiesHolder.Hold(itemToUpdate);
            return Task.FromResult(itemToUpdate);
        }
        public Task<StockItem> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            // пока что мок, что мы что-то нашли.
            var foundStockItem =  new StockItem(
                new Sku(1),
                new Name(""),
                new Item(new ItemType(123, "name")),
                new ClothingSize(1,"size"),
                new Quantity(2),
                new QuantityValue(3));
            // Добавление после успешно выполненной операции.
            _entitiesHolder.Hold(foundStockItem);
            return Task.FromResult(foundStockItem);
        }

        public Task<StockItem> FindBySkuAsync(Sku sku, CancellationToken cancellationToken = default)
        {
            // пока что мок, что мы что-то нашли.
            var foundStockItem =  new StockItem(
                new Sku(1),
                new Name(""),
                new Item(new ItemType(123, "name")),
                new ClothingSize(1,"size"),
                new Quantity(2),
                new QuantityValue(3));
            // Добавление после успешно выполненной операции.
            _entitiesHolder.Hold(foundStockItem);
            return Task.FromResult(foundStockItem);
        }
    }
}