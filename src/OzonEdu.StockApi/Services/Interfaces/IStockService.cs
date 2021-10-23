using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.StockApi.Models;

namespace OzonEdu.StockApi.Services.Interfaces
{
    public interface IStockService
    {
        Task<List<StockItem>> GetAll(CancellationToken token);

        Task<StockItem> GetById(long itemId, CancellationToken _);
        
        Task<StockItem> Add(StockItemCreationModel stockItem, CancellationToken _);
    }
}