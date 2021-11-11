using System.Collections.Generic;
using MediatR;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Commands
{
    public class CreateDeliveryRequestCommand : IRequest<int>, IItemsModel<DeliveryRequestDto>
    {
        public IReadOnlyList<DeliveryRequestDto> Items { get; set; }
    }
}