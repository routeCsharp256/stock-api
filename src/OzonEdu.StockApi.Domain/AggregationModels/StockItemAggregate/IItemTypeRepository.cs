using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.StockApi.Domain.Contracts;

namespace OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate
{
    // TODO: Читерская штука просто для демонстрации.
    public interface IItemTypeRepository : IRepository
    {
        Task<IEnumerable<ItemType>> GetAllTypes(CancellationToken token);
    }
}