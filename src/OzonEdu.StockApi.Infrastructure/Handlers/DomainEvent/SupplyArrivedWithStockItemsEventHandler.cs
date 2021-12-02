using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using CSharpCourse.Core.Lib.Events;
using CSharpCourse.Core.Lib.Models;
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
            return _producerBuilderWrapper.Producer.ProduceAsync(_producerBuilderWrapper.StockReshippedTopic,
                new Message<string, string>()
                {
                    Key = notification.StockItemSku.Value.ToString(),
                    Value = JsonSerializer.Serialize(new StockReplenishedEvent()
                    {
                        Type = new []{new StockReplenishedItem()
                        {
                            Sku = notification.StockItemSku.Value,
                            ItemTypeId = notification.ItemType.Id,
                            ItemTypeName = notification.ItemType.Name,
                            ClothingSize = notification.ClothingSize?.Id
                        }}
                    })
                }, cancellationToken);
        }
    }
}