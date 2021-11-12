using MediatR;

namespace OzonEdu.StockApi.Infrastructure.Commands.GiveOutStockItem
{
    public class GiveOutStockItemCommand : IRequest
    {
        public long Sku { get; set; }
        public int Quantity { get; set; }
    }
}