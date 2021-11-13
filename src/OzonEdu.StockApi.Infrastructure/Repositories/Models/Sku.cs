using System.Diagnostics.CodeAnalysis;

namespace OzonEdu.StockApi.Infrastructure.Repositories.Models
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Sku
    {
        public long Id { get; set; }
        
        public string Name { get; set; }

        public int ItemTypeId { get; set; }

        public int ClothingSize { get; set; }
    }
}