namespace OzonEdu.StockApi.HttpModels
{
    public record StockItemPostViewModel
    {
        public long Sku { get; init; }
        
        public string Name { get; init; }

        public int StockItemType { get; init; }

        public int? ClothingSize { get; init; }

        public int Quantity { get; init; }

        public int? MinimalQuantity { get; init; }

        public string Tags { get; init; }
    }
}