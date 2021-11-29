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
        public ItemType ItemType { get; set; }
        public Quantity Quantity { get; }

        public ClothingSize ClothingSize { get; }

        public SupplyArrivedWithStockItemsDomainEvent(Sku stockItemSku,
            ItemType itemType,
            Quantity quantity,
            ClothingSize clothingSize)
        {
            StockItemSku = stockItemSku;
            ItemType = itemType;
            Quantity = quantity;
            ClothingSize = clothingSize;
        }
    }
}