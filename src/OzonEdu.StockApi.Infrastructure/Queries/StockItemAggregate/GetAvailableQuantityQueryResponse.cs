using System.Collections.Generic;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Queries
{
    public class GetAvailableQuantityQueryResponse : IItemsModel<StockItemQuantityDto>
    {
        public IReadOnlyList<StockItemQuantityDto> Items { get; set; }
    }
}