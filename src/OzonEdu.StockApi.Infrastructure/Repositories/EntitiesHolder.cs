using System.Collections.Concurrent;
using System.Collections.Generic;
using OzonEdu.StockApi.Domain.Models;

namespace OzonEdu.StockApi.Infrastructure.Repositories
{
    public class EntitiesHolder : IEntitiesHolder
    {
        public IEnumerable<Entity> UsedEntities => _usedEntitiesBackingField.ToArray();

        // Можно заменить на любую другую имплементацию. Не только через ConcurrentBag
        private readonly ConcurrentBag<Entity> _usedEntitiesBackingField;

        public EntitiesHolder(ConcurrentBag<Entity> usedEntitiesBackingField)
        {
            _usedEntitiesBackingField = usedEntitiesBackingField;
        }
        
        public void Hold(Entity entity)
        {
            _usedEntitiesBackingField.Add(entity);
        }
    }
}