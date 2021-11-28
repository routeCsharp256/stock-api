using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate;
using OzonEdu.StockApi.Enums;
using OzonEdu.StockApi.Infrastructure.Models;
using OzonEdu.StockApi.Infrastructure.Queries.DeliveryRequestAggregate;
using OzonEdu.StockApi.Infrastructure.Queries.DeliveryRequestAggregate.Responses;

namespace OzonEdu.StockApi.Infrastructure.Handlers.DeliveryRequestAggregate
{
    public class GetDeliveryRequestsQueryHandler : IRequestHandler<GetDeliveryRequestsQuery, DeliveryRequestsQueryResponse>
    {
        private readonly IDeliveryRequestRepository _deliveryRequestRepository;

        public GetDeliveryRequestsQueryHandler(IDeliveryRequestRepository deliveryRequestRepository)
        {
            _deliveryRequestRepository = deliveryRequestRepository;
        }

        public async Task<DeliveryRequestsQueryResponse> Handle(GetDeliveryRequestsQuery request, CancellationToken cancellationToken)
        {
            var fromDb = await _deliveryRequestRepository
                .GetRequestsByStatusAsync(new RequestStatus((int)request.Status, ""), cancellationToken);

            return new DeliveryRequestsQueryResponse()
            {
                Items = fromDb.Select(it => new DeliveryRequestItem()
                {
                    Id = it.Id,
                    RequestStatus = (DeliveryRequestStatus)it.RequestStatus.Id,
                    SkusCollection = it.SkuCollection.Select(it => it.Value).ToArray(),
                    DeliveryRequestId = it.RequestNumber.Value
                }).ToArray()
            };
        }
    }
}