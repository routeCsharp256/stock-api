using Microsoft.Extensions.Configuration;
using OzonEdu.StockApi.HostedServices;
using OzonEdu.StockApi.Infrastructure.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<KafkaConfiguration>(configuration);
            
            return services;
        }
        
        public static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<SupplyConsumerHostedService>();

            return services;
        }

        public static IServiceCollection AddExternalServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSupplGrpcServiceClient(configuration);
            
            return services;
        }
    }
}