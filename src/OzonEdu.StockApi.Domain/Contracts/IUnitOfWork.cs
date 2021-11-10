using System;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;

namespace OzonEdu.StockApi.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IDeliveryRequestRepository DeliveryRequestRepository { get; }

        IStockItemRepository StockItemRepository { get; }
        
        Task CreateDbConnection(CancellationToken token);
        
        ValueTask CreateTransaction(CancellationToken token);
        
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}