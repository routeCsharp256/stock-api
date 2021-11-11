using System.Collections.Generic;

namespace OzonEdu.StockApi.Infrastructure.Models
{
    public class DeliveryRequestItem
    {
        public int Id { get; set; }
        public int DeliveryRequestId { get; set; }
        public DeliveryRequestStatus RequestStatus { get; set; }
        public IReadOnlyList<long> SkusCollection { get; set; }
    }
}