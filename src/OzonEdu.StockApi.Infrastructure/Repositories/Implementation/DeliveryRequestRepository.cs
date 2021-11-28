using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate;
using OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure.Interfaces;
using DeliveryRequest = OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate.DeliveryRequest;

namespace OzonEdu.StockApi.Infrastructure.Repositories.Implementation
{
    public class DeliveryRequestRepository : IDeliveryRequestRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IQueryExecutor _queryExecutor;
        private const int Timeout = 5;

        public DeliveryRequestRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IQueryExecutor queryExecutor)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _queryExecutor = queryExecutor;
        }

        public async Task<DeliveryRequest> CreateAsync(DeliveryRequest itemToCreate,
            CancellationToken cancellationToken)    
        {
            const string sql = @"
                with rows as (
                INSERT INTO delivery_requests (request_id, request_status) 
                VALUES (@RequestId, @RequestStatus) RETURNING id
                )
                INSERT INTO delivery_request_sku_maps (delivery_requests_id, sku_id)
                SELECT id, @SkuId
                FROM rows;";

            var parameters = new
            {
                RequestId = itemToCreate.RequestNumber.Value,
                RequestStatus = itemToCreate.RequestStatus.Id,
                SkuId = itemToCreate.SkuCollection.FirstOrDefault()?.Value ?? 0
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);

            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);

            return await _queryExecutor.Execute(itemToCreate, () => connection.ExecuteAsync(commandDefinition));
        }

        public async Task<DeliveryRequest> FindByIdAsync(int id, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT dr.id, dr.request_id, dr.request_status,
                       drs_skus.sku_id
                FROM delivery_requests AS dr
                LEFT JOIN delivery_request_sku_maps as drs_skus on drs_skus.delivery_requests_id = dr.id
                WHERE dr.id = @DeliveryRequestId;";

            var parameters = new
            {
                DeliveryRequestId = id,
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            return await _queryExecutor.Execute(
                async () =>
                {
                    var stockItems = await connection
                        .QueryAsync<Models.DeliveryRequest, IEnumerable<Models.DeliveryRequestSkuMap>, DeliveryRequest>(
                            commandDefinition,
                            (deliveryRequest, deliveryRequestSkuMap) => new DeliveryRequest(
                                new RequestNumber(deliveryRequest.RequestId),
                                new RequestStatus(deliveryRequest.RequestStatus, ""),
                                deliveryRequestSkuMap
                                    .Select(it => new Domain
                                        .AggregationModels
                                        .ValueObjects.Sku(it.SkuId))
                                    .ToList()));
                    return stockItems.First();
                });
        }

        public async Task<DeliveryRequest> FindByRequestNumberAsync(RequestNumber requestNumber,
            CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT dr.id, dr.request_id, dr.request_status,
                       drs_skus.sku_id
                FROM delivery_requests AS dr
                LEFT JOIN delivery_request_sku_maps as drs_skus on drs_skus.delivery_requests_id = dr.id
                WHERE dr.request_id = @RequestId;";

            var parameters = new
            {
                RequestId = requestNumber.Value,
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            return await _queryExecutor.Execute(
                async () =>
                {
                    var stockItems = await connection
                        .QueryAsync<Models.DeliveryRequest, IEnumerable<Models.DeliveryRequestSkuMap>, DeliveryRequest>(
                            commandDefinition,
                            (deliveryRequest, deliveryRequestSkuMap) => new DeliveryRequest(
                                new RequestNumber(deliveryRequest.RequestId),
                                new RequestStatus(deliveryRequest.RequestStatus, ""),
                                deliveryRequestSkuMap
                                    .Select(it => new Domain
                                        .AggregationModels
                                        .ValueObjects.Sku(it.SkuId))
                                    .ToList()));
                    return stockItems.First();
                });
        }

        public async Task<IReadOnlyCollection<DeliveryRequest>> GetAllRequestsAsync(CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT dr.id, dr.request_id, dr.request_status,
                       drs_skus.sku_id
                FROM delivery_requests AS dr
                LEFT JOIN delivery_request_sku_maps as drs_skus on drs_skus.delivery_requests_id = dr.id";

            var commandDefinition = new CommandDefinition(
                sql,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var result = await _queryExecutor.Execute(
                () => connection
                    .QueryAsync<Models.DeliveryRequest, IEnumerable<Models.DeliveryRequestSkuMap>, DeliveryRequest>(
                        commandDefinition,
                        (deliveryRequest, deliveryRequestSkuMap) => new DeliveryRequest(
                            new RequestNumber(deliveryRequest.RequestId),
                            new RequestStatus(deliveryRequest.RequestStatus, ""),
                            deliveryRequestSkuMap
                                .Select(it => new Domain
                                    .AggregationModels
                                    .ValueObjects.Sku(it.SkuId))
                                .ToList())));
            
            return result.ToArray();
        }
        
        public async Task<IReadOnlyCollection<DeliveryRequest>> GetRequestsByStatusAsync(RequestStatus requestStatus,
            CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT dr.id, dr.request_id, dr.request_status,
                       drs_skus.delivery_requests_id, drs_skus.sku_id
                FROM delivery_requests AS dr
                INNER JOIN delivery_request_sku_maps as drs_skus on drs_skus.delivery_requests_id = dr.id
                WHERE dr.request_status = @RequestStatusId;";

            var parameters = new
            {
                RequestStatusId = requestStatus.Id,
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            
            var result = await _queryExecutor.Execute(
                () => connection
                    .QueryAsync<Models.DeliveryRequest, IEnumerable<Models.DeliveryRequestSkuMap>, DeliveryRequest>(
                        commandDefinition,
                        (deliveryRequest, deliveryRequestSkuMap) => new DeliveryRequest(
                            new RequestNumber(deliveryRequest.RequestId),
                            new RequestStatus(deliveryRequest.RequestStatus, ""),
                            deliveryRequestSkuMap
                                .Select(it => new Domain
                                    .AggregationModels
                                    .ValueObjects.Sku(it.SkuId))
                                .ToList()), splitOn: "delivery_requests_id"));
            
            return result.ToArray();
        }
    }
}