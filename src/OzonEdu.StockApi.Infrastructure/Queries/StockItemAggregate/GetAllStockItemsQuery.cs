using MediatR;
using OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate.Responses;

namespace OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate
{
    public class GetAllStockItemsQuery : IRequest<GetAllStockItemsQueryResponse>
    {
    }
}