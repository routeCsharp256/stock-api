using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Queries
{
    public class DeliveryRequestsQueryResponse
    {
        public int Id { get; set; }
        public DeliveryRequestStatus RequestStatus { get; set; }
    }
}