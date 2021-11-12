using System.Collections.Generic;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Queries.Responses
{
    public class GetAllStockItemsQueryResponse
    {
        public IReadOnlyList<StockItemDto> Items { get; set; }
    }
}