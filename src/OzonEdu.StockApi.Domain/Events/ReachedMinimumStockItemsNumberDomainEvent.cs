using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;

namespace OzonEdu.StockApi.Domain.Events
{
    public class ReachedMinimumStockItemsNumberDomainEvent : INotification
    {
        public Sku StockItemSku { get; }

        public ReachedMinimumStockItemsNumberDomainEvent(Sku stockItemSku)
        {
            StockItemSku = stockItemSku;
        }
    }
}