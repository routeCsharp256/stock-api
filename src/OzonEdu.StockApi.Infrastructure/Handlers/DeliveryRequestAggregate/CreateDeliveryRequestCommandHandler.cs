using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.StockApi.Infrastructure.Commands;

namespace OzonEdu.StockApi.Infrastructure.Handlers
{
    public class CreateDeliveryRequestCommandHandler : IRequestHandler<CreateDeliveryRequestCommand, int>
    {
        public readonly IDeliveryRequestRepository _deliveryRequestRepository;
        
        public CreateDeliveryRequestCommandHandler(IDeliveryRequestRepository deliveryRequestRepository)
        {
            _deliveryRequestRepository = deliveryRequestRepository ?? 
                                         throw new ArgumentNullException($"{nameof(deliveryRequestRepository)}");
        }
        
        public async Task<int> Handle(CreateDeliveryRequestCommand request, CancellationToken cancellationToken)
        {
            var deliveryRequest = new DeliveryRequest(
                null,
                RequestStatus.InWork,
                request.Items.Select(it => new Sku(it.Sku)).ToList());
            
            //TODO Тут должен быть запрос к сервису поставок для получения идентификатора поставки
            // и этот идентификатор нужно будет проставить в модель

            var result = await _deliveryRequestRepository.CreateAsync(deliveryRequest, cancellationToken);
            await _deliveryRequestRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            
            return result.Id;
        }
    }
}