using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.Events;
using OzonEdu.StockApi.Infrastructure.MessageBroker;

namespace OzonEdu.StockApi.Infrastructure.Handlers.DomainEvent
{
    public class SupplyArrivedWithStockItemsEventHandler : INotificationHandler<SupplyArrivedWithStockItemsDomainEvent>
    {
        private readonly IProducerBuilderWrapper _producerBuilderWrapper;

        public SupplyArrivedWithStockItemsEventHandler(IProducerBuilderWrapper producerBuilderWrapper)
        {
            _producerBuilderWrapper = producerBuilderWrapper;
        }

        public Task Handle(SupplyArrivedWithStockItemsDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}