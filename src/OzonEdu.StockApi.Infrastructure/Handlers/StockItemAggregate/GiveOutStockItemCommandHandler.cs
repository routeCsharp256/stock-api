using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.StockApi.Infrastructure.Commands;

namespace OzonEdu.StockApi.Infrastructure.Handlers
{
    public class GiveOutStockItemCommandHandler : IRequestHandler<GiveOutStockItemCommand>
    {
        private readonly IStockItemRepository _stockItemRepository;

        public GiveOutStockItemCommandHandler(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        public async Task<Unit> Handle(GiveOutStockItemCommand request, CancellationToken cancellationToken)
        {
            // Создать метод по нескольким
            var stockItems = await _stockItemRepository.FindBySkusAsync(request.Items.Select(it => new Sku(it.Sku)).ToList(), cancellationToken);

            foreach (var stockItem in stockItems)
            {
                stockItem.GiveOutItems(request.Items.FirstOrDefault(it => it.Sku.Equals(stockItem.Sku.Value))?.Quantity ?? 0);
                await _stockItemRepository.UpdateAsync(stockItem, cancellationToken);
            }
            
            await _stockItemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}