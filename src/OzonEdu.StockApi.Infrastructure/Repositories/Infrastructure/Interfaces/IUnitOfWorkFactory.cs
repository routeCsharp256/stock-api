using System.Threading;
using System.Threading.Tasks;
using OzonEdu.StockApi.Domain.Contracts;

namespace OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure.Interfaces
{
    /// <summary>
    /// Фабрика создания UOW контекстов.
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        Task<IAggregateUnitOfWork> Create(CancellationToken token);
    }
}