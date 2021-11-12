using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.StockApi.Domain.Contracts;
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
            await _unitOfWork.StartTransaction(cancellationToken);
            var stockItem = await _stockItemRepository.FindBySkuAsync(new Sku(request.Sku), cancellationToken);
            if (stockItem is null)
                throw new Exception($"Not found with sku {request.Sku}");

            stockItem.GiveOutItems(request.Quantity);
            await _stockItemRepository.UpdateAsync(stockItem, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}