using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.StockApi.Models;
using OzonEdu.StockApi.Services.Interfaces;

namespace OzonEdu.StockApi.Services
{
    public class StockService : IStockService
    {
        private readonly List<StockItem> StockItems = new List<StockItem>
        {
            new StockItem(1, "Футболка", 10),
            new StockItem(2, "Толстовка", 20),
            new StockItem(3, "Кепка", 15)
        };

        public Task<List<StockItem>> GetAll(CancellationToken _) => Task.FromResult(StockItems);

        public Task<StockItem> GetById(long itemId, CancellationToken _)
        {
            var stockItem = StockItems.FirstOrDefault(x => x.ItemId == itemId);
            return Task.FromResult(stockItem);
        }

        public Task<StockItem> Add(StockItemCreationModel stockItem, CancellationToken _)
        {
            var itemId = StockItems.Max(x => x.ItemId) + 1;
            var newStockItem = new StockItem(itemId, stockItem.ItemName, stockItem.Quantity);
            StockItems.Add(newStockItem);
            return Task.FromResult(newStockItem);
        }
    }
}