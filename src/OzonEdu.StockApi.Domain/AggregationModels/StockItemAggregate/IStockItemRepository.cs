using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.StockApi.Domain.Contracts;

namespace OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate
{
    public interface IStockItemRepository : IRepository<StockItem>
    {
        Task<StockItem> CreateAsync(StockItem itemToCreate, CancellationToken cancellationToken);

        Task<StockItem> UpdateAsync(StockItem itemToUpdate, CancellationToken cancellationToken);

        Task<StockItem> FindBySkuAsync(Sku sku, CancellationToken cancellationToken);

        Task<IReadOnlyList<StockItem>> FindBySkusAsync(IReadOnlyList<Sku> sku, CancellationToken cancellationToken);

        Task<IReadOnlyList<StockItem>> GetAllAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<StockItem>> FindByItemTypeAsync(long itemTypeId, CancellationToken cancellationToken);
    }
}