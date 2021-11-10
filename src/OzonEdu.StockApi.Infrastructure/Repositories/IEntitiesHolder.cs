using System.Collections.Generic;
using OzonEdu.StockApi.Domain.Models;

namespace OzonEdu.StockApi.Infrastructure.Repositories
{
    public interface IEntitiesHolder
    {
        IEnumerable<Entity> UsedEntities { get; }

        public void Hold(Entity entity);
    }
}