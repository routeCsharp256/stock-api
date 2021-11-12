using System.Threading;
using System.Threading.Tasks;
using OzonEdu.StockApi.Domain.Models;

namespace OzonEdu.StockApi.Domain.Contracts
{
    public interface IRepository
    {
    }

    /// <summary>
    /// Базовый интерфейс репозитория
    /// </summary>
    /// <typeparam name="TEntity">Объект сущности для управления</typeparam>
    public interface IRepository<TEntity> : IRepository where TEntity : Entity
    {
        /// <summary>
        /// Создать новую сущность
        /// </summary>
        /// <param name="itemToCreate">Объект для создания</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Созданная сущность</returns>
        Task<TEntity> CreateAsync(TEntity itemToCreate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Обновить существующую сущность
        /// </summary>
        /// <param name="itemToUpdate">Объект для создания</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Обновленная сущность сущность</returns>
        Task<TEntity> UpdateAsync(TEntity itemToUpdate, CancellationToken cancellationToken = default);
    }
}