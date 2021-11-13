using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
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

        public CreateDeliveryRequestCommandHandler(IDeliveryRequestRepository deliveryRequestRepository, IUnitOfWork unitOfWork)
        {
            _deliveryRequestRepository = deliveryRequestRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<int> Handle(CreateDeliveryRequestCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.StartTransaction(cancellationToken);
            var deliveryRequest = new DeliveryRequest(
                null,
                RequestStatus.InWork,
                request.Items.Select(it => new Sku(it.Sku)).ToList());

            //TODO Тут должен быть запрос к сервису поставок для получения идентификатора поставки
            // и этот идентификатор нужно будет проставить в модель

            var result = await _deliveryRequestRepository.CreateAsync(deliveryRequest, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Id;
        }
    }
}