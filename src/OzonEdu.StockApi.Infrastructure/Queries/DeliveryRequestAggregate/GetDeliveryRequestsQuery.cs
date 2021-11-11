using MediatR;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Queries
{
    public class GetDeliveryRequestsQuery : IRequest<DeliveryRequestsQueryResponse>
    {
        public DeliveryRequestStatus Status { get; set; }
    }
}