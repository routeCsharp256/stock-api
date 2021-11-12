using MediatR;
using OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate.Responses;

namespace OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate
{
    public class GetByItemTypeQuery : IRequest<GetByItemTypeQueryResponse>
    {
        /// <summary>
        /// Item type id
        /// </summary>
        public int Id { get; set; }
    }
}