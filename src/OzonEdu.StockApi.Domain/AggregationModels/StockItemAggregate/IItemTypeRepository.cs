using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.StockApi.Domain.Contracts;

namespace OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate
{
    public interface IItemTypeRepository : IRepository<Item>
    {
        Task<IEnumerable<Item>> GetAllTypes(CancellationToken token);
    }
}