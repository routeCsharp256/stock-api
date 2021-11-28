using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Ozon.DotNetCourse.SupplyService.GRPC;
using OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Domain.Contracts;
using OzonEdu.StockApi.Infrastructure.Configuration;
using OzonEdu.StockApi.Infrastructure.MessageBroker;
using OzonEdu.StockApi.Infrastructure.Repositories.Implementation;
using OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure;
using OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseConnection(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<DatabaseConnectionOptions>(configuration.GetSection(nameof(DatabaseConnectionOptions)));
            
            services.AddScoped<IDbConnectionFactory<NpgsqlConnection>, NpgsqlConnectionFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IChangeTracker, ChangeTracker>();
            services.AddScoped<IQueryExecutor, QueryExecutor>();

            return services;
        }
        
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            services.AddScoped<IStockItemRepository, StockItemRepository>();
            services.AddScoped<IDeliveryRequestRepository, DeliveryRequestRepository>();
            services.AddScoped<IItemTypeRepository, ItemTypeRepository>();

            return services;
        }
        
        public static IServiceCollection AddSupplGrpcServiceClient(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionAddress = configuration.GetSection(nameof(SupplyApiGrpcServiceConfiguration))
                .Get<SupplyApiGrpcServiceConfiguration>().ServerAddress;
            if(string.IsNullOrWhiteSpace(connectionAddress))
                connectionAddress = configuration
                    .Get<SupplyApiGrpcServiceConfiguration>()
                    .ServerAddress;

            services.AddScoped<SupplyService.SupplyServiceClient>(opt =>
            {
                var channel = GrpcChannel.ForAddress(connectionAddress);
                return new SupplyService.SupplyServiceClient(channel);
            });


            return services;
        }
        
        public static IServiceCollection AddKafkaServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<KafkaConfiguration>(configuration);
            services.AddScoped<IProducerBuilderWrapper, ProducerBuilderWrapper>();

            return services;
        }
    }
}