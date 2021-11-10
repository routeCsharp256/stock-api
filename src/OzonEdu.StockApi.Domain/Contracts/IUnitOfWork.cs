using System;
using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.StockApi.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ValueTask CreateTransaction(CancellationToken token);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}