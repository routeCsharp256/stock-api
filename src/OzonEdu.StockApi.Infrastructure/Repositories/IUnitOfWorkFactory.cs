using System.Threading;
using System.Threading.Tasks;
using OzonEdu.StockApi.Domain.Contracts;

namespace OzonEdu.StockApi.Infrastructure.Repositories
{
    public interface IUnitOfWorkFactory
    {
        Task<IAggregateUnitOfWork> Create(CancellationToken token);
    }
}