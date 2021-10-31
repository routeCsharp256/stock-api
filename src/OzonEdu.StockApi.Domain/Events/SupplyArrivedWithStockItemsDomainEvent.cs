using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;

namespace OzonEdu.StockApi.Domain.Events
{
    /// <summary>
    /// Пришла поставка с новыми товарами
    /// </summary>
    public class SupplyArrivedWithStockItemsDomainEvent : INotification
    {
        public Sku StockItemSku { get; }
        public Quantity Quantity { get; }

        public SupplyArrivedWithStockItemsDomainEvent(Sku stockItemSku,
            Quantity quantity)
        {
            StockItemSku = stockItemSku;
            Quantity = quantity;
        }
    }
}