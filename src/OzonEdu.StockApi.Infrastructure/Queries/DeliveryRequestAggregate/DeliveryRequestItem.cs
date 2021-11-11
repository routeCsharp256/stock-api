using System.Collections.Generic;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Queries
{
    public class DeliveryRequestItem
    {
        public int Id { get; set; }
        public int DeliveryRequestId { get; set; }
        public DeliveryRequestStatus RequestStatus { get; set; }
        public IReadOnlyList<long> SkusCollection { get; set; }
    }
}