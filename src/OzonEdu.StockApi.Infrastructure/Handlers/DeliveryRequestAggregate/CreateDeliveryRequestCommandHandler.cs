using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ozon.DotNetCourse.SupplyService.GRPC;
using OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.StockApi.Domain.Contracts;
using OzonEdu.StockApi.Infrastructure.Commands.CreateDeliveryRequest;

namespace OzonEdu.StockApi.Infrastructure.Handlers.DeliveryRequestAggregate
{
    public class CreateDeliveryRequestCommandHandler : IRequestHandler<CreateDeliveryRequestCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDeliveryRequestRepository _deliveryRequestRepository;
        private readonly SupplyService.SupplyServiceClient _supplyServiceClient;

        public CreateDeliveryRequestCommandHandler(IDeliveryRequestRepository deliveryRequestRepository,
            IUnitOfWork unitOfWork,
            SupplyService.SupplyServiceClient supplyServiceClient)
        {
            _deliveryRequestRepository = deliveryRequestRepository;
            _unitOfWork = unitOfWork;
            _supplyServiceClient = supplyServiceClient;
        }

        public async Task<int> Handle(CreateDeliveryRequestCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.StartTransaction(cancellationToken);
            var deliveryRequest = new DeliveryRequest(
                null,
                RequestStatus.InWork,
                request.Items.Select(it => new Sku(it.Sku)).ToList());

            // Отправка запроса в сервис Supply для получения номера заказа но поставку
            var supplyRequestItems = request.Items
                .Select(it => new RequestSupplyRequest.Types.SupplyItem()
                {
                    Quantity = it.Quantity,
                    Sku = it.Sku
                }).ToArray();
            var supplyRequest = new RequestSupplyRequest();
            supplyRequest.Items.AddRange(supplyRequestItems);
            var requestNumberResponse = await _supplyServiceClient.RequestSupplyAsync(supplyRequest, cancellationToken: cancellationToken);
            
            deliveryRequest.SetRequestNumber(requestNumberResponse.SupplyId);

            var result = await _deliveryRequestRepository.CreateAsync(deliveryRequest, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Id;
        }
    }
}