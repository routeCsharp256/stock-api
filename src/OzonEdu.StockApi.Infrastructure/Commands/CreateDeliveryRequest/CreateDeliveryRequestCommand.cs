using System.Collections.Generic;
using MediatR;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Commands.CreateDeliveryRequest
{
    public class CreateDeliveryRequestCommand : IRequest<long>, IItemsModel<DeliveryRequestDto>
    {
        public IReadOnlyList<DeliveryRequestDto> Items { get; set; }
    }
}