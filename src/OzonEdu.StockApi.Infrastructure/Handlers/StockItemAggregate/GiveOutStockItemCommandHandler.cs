using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.StockApi.Infrastructure.Commands.GiveOutStockItem;
using OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.StockApi.Infrastructure.Handlers.StockItemAggregate
{
    public class GiveOutStockItemCommandHandler : IRequestHandler<GiveOutStockItemCommand>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public GiveOutStockItemCommandHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<Unit> Handle(GiveOutStockItemCommand request, CancellationToken cancellationToken)
        {
            using var uow = await _unitOfWorkFactory.Create(cancellationToken);
            var stockItem = await uow.StockItemRepository.FindBySkuAsync(new Sku(request.Sku), cancellationToken);
            if (stockItem is null)
                throw new Exception($"Not found with sku {request.Sku}");

            stockItem.GiveOutItems(request.Quantity);
            await uow.StockItemRepository.UpdateAsync(stockItem, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}