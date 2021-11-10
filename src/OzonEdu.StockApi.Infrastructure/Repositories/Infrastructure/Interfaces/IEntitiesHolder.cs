using System.Collections.Generic;
using OzonEdu.StockApi.Domain.Models;

namespace OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure.Interfaces
{
    /// <summary>
    /// Компонент, ответственный за хранение ссылок на сущности, которые были затронуты в рамках выполнения запроса.
    /// </summary>
    /// <remarks>
    /// Необходим для сбора доменных событий при сохранении.
    /// </remarks>
    public interface IEntitiesHolder
    {
        /// <summary>
        /// Коллекция всех сущностей, которые так или иначе были использованы в репозитории.
        /// </summary>
        IEnumerable<Entity> UsedEntities { get; }

        /// <summary>
        /// "Записать" сущность как подлежащую "использованию" в рамках выполнения запроса.
        /// </summary>
        /// <param name="entity"></param>
        public void Hold(Entity entity);
    }
}