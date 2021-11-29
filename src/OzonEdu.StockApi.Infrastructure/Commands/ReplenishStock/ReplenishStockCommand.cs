using System.Collections.Generic;
using MediatR;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Commands.ReplenishStock
{
    public class ReplenishStockCommand : IRequest
    {
        public long SupplyId { get; set; }
        public IReadOnlyCollection<StockItemQuantityDto> Items { get; set; }
    }
}