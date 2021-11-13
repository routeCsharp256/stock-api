using MediatR;
using OzonEdu.StockApi.Enums;
using OzonEdu.StockApi.Infrastructure.Models;
using OzonEdu.StockApi.Infrastructure.Queries.DeliveryRequestAggregate.Responses;

namespace OzonEdu.StockApi.Infrastructure.Queries.DeliveryRequestAggregate
{
    public class GetDeliveryRequestsQuery : IRequest<DeliveryRequestsQueryResponse>
    {
        public DeliveryRequestStatus Status { get; set; }
    }
}