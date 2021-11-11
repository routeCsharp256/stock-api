using MediatR;
using OzonEdu.StockApi.Infrastructure.Models;
using OzonEdu.StockApi.Infrastructure.Queries.Responses;

namespace OzonEdu.StockApi.Infrastructure.Queries
{
    public class GetDeliveryRequestsQuery : IRequest<DeliveryRequestsQueryResponse>
    {
        public DeliveryRequestStatus Status { get; set; }
    }
}