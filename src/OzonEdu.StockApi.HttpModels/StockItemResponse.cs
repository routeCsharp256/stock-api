namespace OzonEdu.StockApi.HttpModels
{
    public class StockItemResponse
    {
        public long ItemId { get; set; }
        
        public string ItemName { get; set; }
        
        public int Quantity { get; set; }
    }
}