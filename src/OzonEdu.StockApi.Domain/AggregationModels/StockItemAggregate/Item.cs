using OzonEdu.StockApi.Domain.Models;

namespace OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate
{
    public class Item : Entity
    {
        public ItemType Type { get; }

        public Item(ItemType type)
        {
            Type = type;
        }
    }
}