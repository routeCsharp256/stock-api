namespace OzonEdu.StockApi.Infrastructure.Repositories.Models
{
    public class Sku
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public int ItemTypeId { get; set; }
        
        public int ClothingSize { get; set; }
    }

    public class StockItem
    {
        public long Id { get; set; }
        
        public long SkuId { get; set; }
        
        public int Quantity { get; set; }
        
        public int MinimalQuantity { get; set; }
    }

    public class ItemType
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
    }

    public class ClothingSize
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
    }

    public class DeliveryRequest
    {
        public long Id { get; set; }
        
        public long RequestId { get; set; }
        
        public int RequestStatus { get; set; }
    }

    public class DeliveryRequestSkuMap
    {
        public long DeliveryRequestId { get; set; }

        public long SkuId { get; set; }
    }
}