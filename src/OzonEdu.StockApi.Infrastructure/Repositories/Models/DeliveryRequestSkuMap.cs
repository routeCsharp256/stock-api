using System.Diagnostics.CodeAnalysis;

namespace OzonEdu.StockApi.Infrastructure.Repositories.Models
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class DeliveryRequestSkuMap
    {
        public long DeliveryRequestId { get; set; }

        public long SkuId { get; set; }
    }
}