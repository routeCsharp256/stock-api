using System.Collections.Generic;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Queries.Responses
{
    public class GetItemTypesQueryResponse : IItemsModel<ItemTypeDto>
    {
        public IReadOnlyList<ItemTypeDto> Items { get; set; }
    }
}