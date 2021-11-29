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

        public async Task<DeliveryRequest> CreateAsync(DeliveryRequest itemToCreate, CancellationToken cancellationToken)
        {
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            return await _queryExecutor.Execute(
                itemToCreate,
                async () =>
                {
                    var deliveryRequestId = await connection.QuerySingleAsync<long>(
                        new CommandDefinition(
                            commandText: @"INSERT INTO delivery_requests (request_id, request_status) 
                            VALUES (@RequestId, @RequestStatus) RETURNING id",
                            parameters: new
                            {
                                RequestId = itemToCreate.RequestNumber.Value,
                                RequestStatus = itemToCreate.RequestStatus.Id,
                            },
                            commandTimeout: Timeout,
                            cancellationToken: cancellationToken));

                    var insertSkusToRequestCommand = new CommandDefinition(
                        commandText: @"INSERT INTO delivery_request_sku_maps (delivery_requests_id, sku_id)
                            SELECT @DeliveryRequestsId, UNNEST(@Skus);",
                        parameters: new
                        {
                            DeliveryRequestsId = deliveryRequestId,
                            Skus = itemToCreate.SkuCollection.Select(it => it.Value).ToArray()
                        },
                        commandTimeout: Timeout,
                        cancellationToken: cancellationToken);

                    await connection.ExecuteAsync(insertSkusToRequestCommand);
                });
        }

        public async Task<DeliveryRequest> UpdateAsync(DeliveryRequest itemToUpdate,
            CancellationToken cancellationToken)
        {
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            return await _queryExecutor.Execute(
                itemToUpdate,
                async () =>
                {
                    var deliveryRequestId = await connection.ExecuteAsync(
                        new CommandDefinition(
                            commandText: @"UPDATE delivery_requests 
                            SET request_id = @NewRequestId, request_status = @NewRequestStatus
                            WHERE id = @DelivaryRequestId",
                            parameters: new
                            {
                                DelivaryRequestId = itemToUpdate.Id,
                                NewRequestId = itemToUpdate.RequestNumber.Value,
                                NewRequestStatus = itemToUpdate.RequestStatus.Id,
                            },
                            commandTimeout: Timeout,
                            cancellationToken: cancellationToken));
                });
        }

        public async Task<DeliveryRequest> FindByIdAsync(int id, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT dr.id, dr.request_id, dr.request_status,
                       drs_skus.delivery_requests_id, drs_skus.sku_id
                FROM delivery_requests AS dr
                INNER JOIN delivery_request_sku_maps as drs_skus on drs_skus.delivery_requests_id = dr.id
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
            
            var res = await connection
                .QueryAsync<Models.DeliveryRequest, Models.DeliveryRequestSkuMap, Models.DeliveryRequestSkuMap>(
                    commandDefinition,
                    (deliveryRequest, deliveryRequestSkuMap) =>
                    {
                        deliveryRequestSkuMap.DeliveryRequest = deliveryRequest;
                        return deliveryRequestSkuMap;
                    }, splitOn: "delivery_requests_id");

            var groupedRes = res.GroupBy(it => it.DeliveryRequest.Id)
                .ToArray();

            var result = await _queryExecutor.Execute(() =>
            {
                return Task.FromResult(groupedRes.Select(it =>
                {
                    var deliveryRequest = it.First();
                    var skus = it.Select(it => it.SkuId).ToList();
                    return DeliveryRequest.CreateInstance(deliveryRequest.DeliveryRequest.Id, deliveryRequest.DeliveryRequest.RequestId,
                        deliveryRequest.DeliveryRequest.RequestStatus, "", skus);
                }));
            });
            
            return result.FirstOrDefault();
        }

        public async Task<DeliveryRequest> FindByRequestNumberAsync(RequestNumber requestNumber,
            CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT dr.id, dr.request_id, dr.request_status,
                       drs_skus.delivery_requests_id, drs_skus.sku_id
                FROM delivery_requests AS dr
                INNER JOIN delivery_request_sku_maps as drs_skus on drs_skus.delivery_requests_id = dr.id
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
            
            var res = await connection
                .QueryAsync<Models.DeliveryRequest, Models.DeliveryRequestSkuMap, Models.DeliveryRequestSkuMap>(
                    commandDefinition,
                    (deliveryRequest, deliveryRequestSkuMap) =>
                    {
                        deliveryRequestSkuMap.DeliveryRequest = deliveryRequest;
                        return deliveryRequestSkuMap;
                    }, splitOn: "delivery_requests_id");

            var groupedRes = res.GroupBy(it => it.DeliveryRequest.Id)
                .ToArray();

            var result = await _queryExecutor.Execute(() =>
            {
                return Task.FromResult(groupedRes.Select(it =>
                {
                    var deliveryRequest = it.First();
                    var skus = it.Select(it => it.SkuId).ToList();
                    return DeliveryRequest.CreateInstance(deliveryRequest.DeliveryRequest.Id, deliveryRequest.DeliveryRequest.RequestId,
                        deliveryRequest.DeliveryRequest.RequestStatus, "", skus);
                }));
            });
            
            return result.FirstOrDefault();
        }

        public async Task<IReadOnlyCollection<DeliveryRequest>> GetAllRequestsAsync(CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT dr.id, dr.request_id, dr.request_status,
                       drs_skus.delivery_requests_id, drs_skus.sku_id
                FROM delivery_requests AS dr
                INNER JOIN delivery_request_sku_maps as drs_skus on drs_skus.delivery_requests_id = dr.id;";

            var commandDefinition = new CommandDefinition(
                sql,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            
            var res = await connection
                .QueryAsync<Models.DeliveryRequest, Models.DeliveryRequestSkuMap, Models.DeliveryRequestSkuMap>(
                    commandDefinition,
                    (deliveryRequest, deliveryRequestSkuMap) =>
                    {
                        deliveryRequestSkuMap.DeliveryRequest = deliveryRequest;
                        return deliveryRequestSkuMap;
                    }, splitOn: "delivery_requests_id");

            var groupedRes = res.GroupBy(it => it.DeliveryRequest.Id)
                .ToArray();

            var result = await _queryExecutor.Execute(() =>
            {
                return Task.FromResult(groupedRes.Select(it =>
                {
                    var deliveryRequest = it.First();
                    var skus = it.Select(it => it.SkuId).ToList();
                    return DeliveryRequest.CreateInstance(deliveryRequest.DeliveryRequest.Id, deliveryRequest.DeliveryRequest.RequestId,
                        deliveryRequest.DeliveryRequest.RequestStatus, "", skus);
                }));
            });

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

            var res = await connection
                .QueryAsync<Models.DeliveryRequest, Models.DeliveryRequestSkuMap, Models.DeliveryRequestSkuMap>(
                    commandDefinition,
                    (deliveryRequest, deliveryRequestSkuMap) =>
                    {
                        deliveryRequestSkuMap.DeliveryRequest = deliveryRequest;
                        return deliveryRequestSkuMap;
                    }, splitOn: "delivery_requests_id");

            var groupedRes = res.GroupBy(it => it.DeliveryRequest.Id)
                .ToArray();

            var result = await _queryExecutor.Execute(() =>
            {
                return Task.FromResult(groupedRes.Select(it =>
                {
                    var deliveryRequest = it.First();
                    var skus = it.Select(it => it.SkuId).ToList();
                    return DeliveryRequest.CreateInstance(deliveryRequest.DeliveryRequest.Id, deliveryRequest.DeliveryRequest.RequestId,
                        deliveryRequest.DeliveryRequest.RequestStatus, "", skus);
                }));
            });

            return result.ToArray();
        }
    }
}