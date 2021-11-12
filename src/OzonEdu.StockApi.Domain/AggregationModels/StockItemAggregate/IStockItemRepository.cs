using System.Collections.Generic;
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
        /// Найти товарную позицию по складскому идентфикатору
        /// </summary>
        /// <param name="sku">Складской идентификатор товарной позиции</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Товарная позиция</returns>
        Task<StockItem> FindBySkuAsync(Sku sku, CancellationToken cancellationToken);
        
        /// <summary>
        /// Найти товарную позицию по складскому идентфикатору
        /// </summary>
        /// <param name="skus">Складской идентификатор товарной позиции</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Товарная позиция</returns>
        Task<IReadOnlyList<StockItem>> FindBySkusAsync(IReadOnlyList<Sku> sku, CancellationToken cancellationToken);


        /// <summary>
        /// Получить все товарные позиции
        /// </summary>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Товарные позиции</returns>
        Task<IReadOnlyList<StockItem>> GetAllAsync(CancellationToken cancellationToken);
    }
}