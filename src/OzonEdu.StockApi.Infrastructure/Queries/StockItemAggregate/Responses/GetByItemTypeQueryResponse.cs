using System.Collections.Generic;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Queries.Responses
{
    public class GetByItemTypeQueryResponse : IItemsModel<StockItemDto>
    {
        public IReadOnlyList<StockItemDto> Items { get; set; }
    }
}