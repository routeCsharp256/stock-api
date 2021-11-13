using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.StockApi.Infrastructure.Repositories.Implementation
{
    public class ItemTypeRepository : IItemTypeRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public ItemTypeRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public async Task<IEnumerable<Item>> GetAllTypes(CancellationToken cancellationToken)
        {

            const string sql = @"
                SELECT item_types.id, item_types.name
                FROM item_types;";
            
            var commandDefinition = new CommandDefinition(
                sql,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var stockItems = await connection.QueryAsync<Models.ItemType>(commandDefinition);
            var result = stockItems.Select(x => new Item(new ItemType(x.Id, x.Name))).ToList();
            foreach (var item in result)
            {
                _changeTracker.Track(item);
            }

            return result;
        }
    }
}