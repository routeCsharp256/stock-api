using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.Events;

namespace OzonEdu.StockApi.Infrastructure.Handlers
{
    public class ReachedMinimumDomainEventHandler : INotificationHandler<ReachedMinimumStockItemsNumberDomainEvent> 
    {
        public Task Handle(ReachedMinimumStockItemsNumberDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}