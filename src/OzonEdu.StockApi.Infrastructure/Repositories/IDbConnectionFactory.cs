using System;
using System.Threading.Tasks;

namespace OzonEdu.StockApi.Infrastructure.Repositories
{
    public interface IDbConnectionFactory<TConnection> : IDisposable
    {
        public TConnection Connection { get; }
        Task<TConnection> CreateConnection();
    }
}