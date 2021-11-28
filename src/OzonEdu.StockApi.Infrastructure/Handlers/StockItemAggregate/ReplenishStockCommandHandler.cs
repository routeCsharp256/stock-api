using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.StockApi.Domain.Contracts;
using OzonEdu.StockApi.Infrastructure.Commands.ReplenishStock;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Handlers.StockItemAggregate
{
    public class ReplenishStockCommandHandler : IRequestHandler<ReplenishStockCommand>
    {
        private readonly IStockItemRepository _stockItemRepository;
        private readonly IDeliveryRequestRepository _deliveryRequestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReplenishStockCommandHandler(IStockItemRepository stockItemRepository, 
            IDeliveryRequestRepository deliveryRequestRepository)
        {
            _stockItemRepository = stockItemRepository;
            _deliveryRequestRepository = deliveryRequestRepository;
        }

        public async Task<Unit> Handle(ReplenishStockCommand request, CancellationToken cancellationToken)
        {
            var stocks = await _stockItemRepository.FindBySkusAsync(request.Items
                    .Select(it => new Sku(it.Sku)).ToArray(),
                cancellationToken);

            await _unitOfWork.StartTransaction(cancellationToken);
            foreach (var stock in stocks)
            {
                var dataToIncrease = request.Items.FirstOrDefault(it =>
                    it.Sku.Equals(stock.Sku.Value)) ?? new StockItemQuantityDto(){Quantity = 0};
                
                stock.IncreaseQuantity(dataToIncrease.Quantity);
                await _stockItemRepository.UpdateAsync(stock, cancellationToken);
            }

            var deliveryRequest = await _deliveryRequestRepository
                .FindByRequestNumberAsync(new RequestNumber(request.SupplyId), cancellationToken);
            deliveryRequest.ChangeStatus(RequestStatus.Done);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}