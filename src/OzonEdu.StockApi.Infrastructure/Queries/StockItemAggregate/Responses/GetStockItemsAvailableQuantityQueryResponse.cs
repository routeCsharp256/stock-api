﻿using System.Collections.Generic;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate.Responses
{
    public class GetStockItemsAvailableQuantityQueryResponse : IItemsModel<StockItemQuantityDto>
    {
        public IReadOnlyList<StockItemQuantityDto> Items { get; set; }
    }
}