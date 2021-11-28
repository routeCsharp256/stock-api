using System.Collections.Generic;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate.Responses
{
    public class GetBySkuIdsQueryResponse
    {
        public IReadOnlyList<StockItemDto> Items { get; set; }
    }
}