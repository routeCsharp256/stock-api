namespace OzonEdu.StockApi.HttpModels
{
    public record DeliveryRequestModel
    {
        public long Sku { get; init; }

        public int Quantity { get; init; }
    }
}