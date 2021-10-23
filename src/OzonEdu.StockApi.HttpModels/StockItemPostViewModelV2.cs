namespace OzonEdu.StockApi.HttpModels
{
    public class StockItemPostViewModelV2
    {
        public string ItemName { get; set; }
        
        public AvailableQuantity Quantity { get; set; }
    }
}