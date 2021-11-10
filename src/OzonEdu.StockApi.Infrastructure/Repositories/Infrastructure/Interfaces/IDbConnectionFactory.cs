using System;
using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure.Interfaces
{
    /// <summary>
    /// Фабрика подключений к базе данных.
    /// </summary>
    public interface IDbConnectionFactory<TConnection> : IDisposable
    {
        /// <summary>
        /// Подключение, которое будет "закешировано" после создания.
        /// </summary>
        public TConnection Connection { get; }

        /// <summary>
        /// Создать подключение к БД.
        /// </summary>
        /// <returns></returns>
        Task<TConnection> CreateConnection(CancellationToken token);
    }
}