using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.StockApi.Infrastructure.Repositories.Implementation
{
    public class StockItemRepository : IStockItemRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public StockItemRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public async Task<StockItem> CreateAsync(StockItem itemToCreate, CancellationToken cancellationToken = default)
        {
            var commandDefinition = new CommandDefinition(
                "",
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
            // Добавление после успешно выполненной операции.
            _changeTracker.Track(itemToCreate);
            return itemToCreate;
        }

        public Task<StockItem> UpdateAsync(StockItem itemToUpdate, CancellationToken cancellationToken)
        {
            // Добавление после успешно выполненной операции.
            _changeTracker.Track(itemToUpdate);
            return Task.FromResult(itemToUpdate);
        }

        public Task<StockItem> FindByIdAsync(long id, CancellationToken cancellationToken)
        {
            // пока что мок, что мы что-то нашли.
            var foundStockItem = new StockItem(
                new Sku(1),
                new Name(""),
                new Item(new ItemType(123, "name")),
                new ClothingSize(1, "size"),
                new Quantity(2),
                new QuantityValue(3));
            // Добавление после успешно выполненной операции.
            _changeTracker.Track(foundStockItem);
            return Task.FromResult(foundStockItem);
        }

        public Task<StockItem> FindBySkuAsync(Sku sku, CancellationToken cancellationToken)
        {
            // пока что мок, что мы что-то нашли.
            var foundStockItem = new StockItem(
                new Sku(1),
                new Name(""),
                new Item(new ItemType(123, "name")),
                new ClothingSize(1, "size"),
                new Quantity(2),
                new QuantityValue(3));
            // Добавление после успешно выполненной операции.
            _changeTracker.Track(foundStockItem);
            return Task.FromResult(foundStockItem);
        }
    }
}