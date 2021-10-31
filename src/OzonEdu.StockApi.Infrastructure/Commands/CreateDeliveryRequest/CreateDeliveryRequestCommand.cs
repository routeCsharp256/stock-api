using System.Collections.Generic;
using MediatR;

namespace OzonEdu.StockApi.Infrastructure.Commands
{
    public class CreateDeliveryRequestCommand : IRequest
    {
        public IReadOnlyList<long> SkuCollection { get; set; }
    }
}