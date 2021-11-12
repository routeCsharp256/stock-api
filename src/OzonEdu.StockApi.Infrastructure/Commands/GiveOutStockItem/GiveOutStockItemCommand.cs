using System.Collections.Generic;
using MediatR;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Commands
{
    public class GiveOutStockItemCommand : IRequest
    {
        public IReadOnlyList<StockItemQuantityDto> Items { get; init; }
    }
}