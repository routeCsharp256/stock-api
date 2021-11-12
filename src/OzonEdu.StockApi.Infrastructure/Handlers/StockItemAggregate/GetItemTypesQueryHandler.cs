using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Domain.Contracts;
using OzonEdu.StockApi.Infrastructure.Models;
using OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate;
using OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate.Responses;

namespace OzonEdu.StockApi.Infrastructure.Handlers.StockItemAggregate
{
    public class GetItemTypesQueryHandler : IRequestHandler<GetItemTypesQuery, GetItemTypesQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItemTypeRepository _itemTypeRepository;

        public GetItemTypesQueryHandler(IItemTypeRepository itemTypeRepository, IUnitOfWork unitOfWork)
        {
            _itemTypeRepository = itemTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetItemTypesQueryResponse> Handle(GetItemTypesQuery request, CancellationToken cancellationToken)
        {
            await _unitOfWork.StartTransaction(cancellationToken);
            var itemTypes = await _itemTypeRepository.GetAllTypes(cancellationToken); 
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new GetItemTypesQueryResponse
            {
                Items = itemTypes.Select(
                        x => new ItemTypeDto
                        {
                            Id = x.Id,
                            Name = x.ToString()
                        })
                    .ToList()
            };
        }
    }
}