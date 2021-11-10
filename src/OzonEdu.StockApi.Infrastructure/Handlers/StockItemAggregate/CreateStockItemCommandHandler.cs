using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.StockApi.Domain.Models;
using OzonEdu.StockApi.Infrastructure.Commands.CreateStockItem;
using OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.StockApi.Infrastructure.Handlers.StockItemAggregate
{
    public class CreateStockItemCommandHandler : IRequestHandler<CreateStockItemCommand, int>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public CreateStockItemCommandHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<int> Handle(CreateStockItemCommand request, CancellationToken cancellationToken)
        {
            using var uow = await _unitOfWorkFactory.Create(cancellationToken);
            var stockInDb = await uow.StockItemRepository
                .FindBySkuAsync(new Sku(request.Sku), cancellationToken);
            if (stockInDb is not null)
                throw new Exception($"Stock item with sku {request.Sku} already exist");

            var newStockItem = new StockItem(
                new Sku(request.Sku),
                new Name(request.Name),
                new Item(ItemType
                    .GetAll<ItemType>()
                    .FirstOrDefault(it => it.Id.Equals(request.StockItemType))),
                Enumeration
                    .GetAll<ClothingSize>()
                    .FirstOrDefault(it => it.Id.Equals(request.ClothingSize)),
                new Quantity(request.Quantity),
                new QuantityValue(request.MinimalQuantity)
                );

            var createResult = await uow.StockItemRepository.CreateAsync(newStockItem, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);
            
            return newStockItem.Id;
        }
    }
}