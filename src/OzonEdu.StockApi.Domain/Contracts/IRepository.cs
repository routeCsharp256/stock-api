using OzonEdu.StockApi.Domain.Models;

namespace OzonEdu.StockApi.Domain.Contracts
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
    }
}