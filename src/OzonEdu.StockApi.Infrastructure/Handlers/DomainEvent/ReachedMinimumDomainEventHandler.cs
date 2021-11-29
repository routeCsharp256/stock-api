using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.DotNetCourse.SupplyService.GRPC;
using OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate;
using OzonEdu.StockApi.Domain.Contracts;
using OzonEdu.StockApi.Domain.Events;

namespace OzonEdu.StockApi.Infrastructure.Handlers.DomainEvent
{
    public class ReachedMinimumDomainEventHandler : INotificationHandler<ReachedMinimumStockItemsNumberDomainEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDeliveryRequestRepository _deliveryRequestRepository;
        private readonly SupplyService.SupplyServiceClient _supplyServiceClient;

        public ReachedMinimumDomainEventHandler(IUnitOfWork unitOfWork,
            IDeliveryRequestRepository deliveryRequestRepository,
            SupplyService.SupplyServiceClient supplyServiceClient)
        {
            _unitOfWork = unitOfWork;
            _deliveryRequestRepository = deliveryRequestRepository;
            _supplyServiceClient = supplyServiceClient;
        }

        public async Task Handle(ReachedMinimumStockItemsNumberDomainEvent notification,
            CancellationToken cancellationToken)
        {
            var deliveryRequest = new DeliveryRequest(
                null,
                RequestStatus.InWork,
                new[] { notification.StockItemSku });

            // Отправка запроса в сервис Supply для получения номера заказа но поставку
            var supplyRequestItems = new[]
            {
                new RequestSupplyRequest.Types.SupplyItem()
                {
                    Quantity = 100,
                    Sku = notification.StockItemSku.Value
                }
            };
            var supplyRequest = new RequestSupplyRequest();
            supplyRequest.Items.AddRange(supplyRequestItems);
            var requestNumberResponse =
                await _supplyServiceClient.RequestSupplyAsync(supplyRequest, cancellationToken: cancellationToken);

            deliveryRequest.SetRequestNumber(requestNumberResponse.SupplyId);

            var result = await _deliveryRequestRepository.CreateAsync(deliveryRequest, cancellationToken);
        }
    }
}