using System.Threading;
using System.Threading.Tasks;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.StockApi.Domain.Contracts;

namespace OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate
{
    /// <summary>
    /// Репозиторий для управления <see cref="StockItem"/>
    /// </summary>
    public interface IStockItemRepository : IRepository<StockItem>
    {
        /// <summary>
        /// Найти товарную позицию по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор товарной позиции</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Товарная позиция</returns>
        Task<StockItem> FindByIdAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Найти товарную позицию по складскому идентфикатору
        /// </summary>
        /// <param name="sku">Складской идентификатор товарной позиции</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Товарная позиция</returns>
        Task<StockItem> FindBySkuAsync(Sku sku, CancellationToken cancellationToken = default);
    }
}