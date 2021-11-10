using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.StockApi.Infrastructure.Commands.CreateDeliveryRequest;
using OzonEdu.StockApi.Infrastructure.Repositories;

namespace OzonEdu.StockApi.Infrastructure.Handlers.DeliveryRequestAggregate
{
    public class CreateDeliveryRequestCommandHandler : IRequestHandler<CreateDeliveryRequestCommand>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public CreateDeliveryRequestCommandHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<Unit> Handle(CreateDeliveryRequestCommand request, CancellationToken cancellationToken)
        {
            using var uow = await _unitOfWorkFactory.Create(cancellationToken);
            var deliveryRequest = new DeliveryRequest(
                null,
                RequestStatus.InWork,
                request.SkuCollection.Select(it => new Sku(it)).ToList());

            //TODO Тут должен быть запрос к сервису поставок для получения идентификатора поставки
            // и этот идентификатор нужно будет проставить в модель

            await uow.DeliveryRequestRepository.CreateAsync(deliveryRequest, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}