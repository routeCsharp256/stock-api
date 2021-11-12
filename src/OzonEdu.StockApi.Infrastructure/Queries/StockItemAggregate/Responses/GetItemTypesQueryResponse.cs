using System.Collections.Generic;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate.Responses
{
    public class GetItemTypesQueryResponse : IItemsModel<ItemTypeDto>
    {
        public IReadOnlyList<ItemTypeDto> Items { get; set; }
    }
}