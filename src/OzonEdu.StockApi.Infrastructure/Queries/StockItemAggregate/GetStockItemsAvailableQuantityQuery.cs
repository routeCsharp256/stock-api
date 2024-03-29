﻿using System.Collections.Generic;
using MediatR;
using OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate.Responses;

namespace OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate
{
    /// <summary>
    /// Полуить доступное количество товарных позиций
    /// </summary>
    public class GetStockItemsAvailableQuantityQuery : IRequest<GetStockItemsAvailableQuantityQueryResponse>
    {
        /// <summary>
        /// Идентификатор товарной позиции
        /// </summary>
        public IReadOnlyList<long> Skus { get; set; }
    }
}