using System.Collections.Generic;
using MediatR;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Queries
{
    /// <summary>
    /// Полуить доступное количество товарных позиций
    /// </summary>
    public class GetAvailableQuantityQuery : IRequest<GetAvailableQuantityQueryResponse>
    {
        /// <summary>
        /// Идентификатор товарной позиции
        /// </summary>
        public IReadOnlyList<long> Skus { get; set; }
    }
}