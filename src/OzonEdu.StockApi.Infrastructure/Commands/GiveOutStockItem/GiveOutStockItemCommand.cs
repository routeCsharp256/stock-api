using System.Collections.Generic;
using MediatR;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Commands.GiveOutStockItem
{
    public class GiveOutStockItemCommand : IRequest<GiveOutStockItemResult>
    {
        public IReadOnlyList<StockItemQuantityDto> Items { get; init; }
    }

    public enum GiveOutStockItemResult
    {
        Successful =0,
        OutOfStock = 1,
    }
}