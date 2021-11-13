using System.Diagnostics.CodeAnalysis;

namespace OzonEdu.StockApi.Infrastructure.Repositories.Models
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class StockItem
    {
        public long Id { get; set; }
        
        public long SkuId { get; set; }
        
        public int Quantity { get; set; }
        
        public int MinimalQuantity { get; set; }
    }
}