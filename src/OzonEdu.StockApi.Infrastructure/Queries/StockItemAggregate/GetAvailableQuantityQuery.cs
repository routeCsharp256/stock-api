using MediatR;

namespace OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate
{
    /// <summary>
    /// Полуить доступное количество товарных позиций
    /// </summary>
    public class GetAvailableQuantityQuery : IRequest<int>
    {
        /// <summary>
        /// Идентификатор товарной позиции
        /// </summary>
        public long Sku { get; set; }
    }
}