using System.Collections.Generic;
using MediatR;
using OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate.Responses;

namespace OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate
{
    public class GetBySkuIdsQuery : IRequest<GetBySkuIdsQueryResponse>
    {
        public IReadOnlyCollection<long> SkuIds { get; set; }
    }
}