using MediatR;
using OzonEdu.StockApi.Infrastructure.Queries.Responses;

namespace OzonEdu.StockApi.Infrastructure.Queries
{
    public class GetByItemTypeQuery : IRequest<GetByItemTypeQueryResponse>
    {
        /// <summary>
        /// Item type id
        /// </summary>
        public int Id { get; set; }
    }
}