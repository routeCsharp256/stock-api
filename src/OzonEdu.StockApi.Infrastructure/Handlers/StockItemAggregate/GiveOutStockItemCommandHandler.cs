using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;
using IUnitOfWork = OzonEdu.StockApi.Domain.Contracts.IUnitOfWork;
using OzonEdu.StockApi.Infrastructure.Commands.GiveOutStockItem;

namespace OzonEdu.StockApi.Infrastructure.Handlers.StockItemAggregate
{
    public class GiveOutStockItemCommandHandler : IRequestHandler<GiveOutStockItemCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockItemRepository _stockItemRepository;

        public GiveOutStockItemCommandHandler(IStockItemRepository stockItemRepository, IUnitOfWork unitOfWork)
        {
            _stockItemRepository = stockItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(GiveOutStockItemCommand request, CancellationToken cancellationToken)
        {
            // Создать метод по нескольким
            await _unitOfWork.StartTransaction(cancellationToken);
            var stockItems = await _stockItemRepository.FindBySkusAsync(
                request.Items.Select(it => new Sku(it.Sku)).ToList(),
                cancellationToken);

            foreach (var stockItem in stockItems)
            {
                stockItem.GiveOutItems(request.Items.FirstOrDefault(it => it.Sku.Equals(stockItem.Sku.Value))?.Quantity ?? 0);
                await _stockItemRepository.UpdateAsync(stockItem, cancellationToken);
            }
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}