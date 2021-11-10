using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.GrpcServices;
using OzonEdu.StockApi.Infrastructure.Configuration;
using OzonEdu.StockApi.Infrastructure.Repositories.Implementation;
using OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure;
using OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.StockApi
{
	public class Startup
	{
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
		public void ConfigureServices(IServiceCollection services)
        {
	        services.AddMediatR(typeof(Startup));
	        services.Configure<DatabaseConnectionOptions>(Configuration.GetSection(nameof(DatabaseConnectionOptions)));
	        services.AddScoped<IDbConnectionFactory<NpgsqlConnection>, NpgsqlConnectionFactory>();
	        services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
	        services.AddScoped<IEntitiesHolder, EntitiesHolder>();
	        services.AddScoped<IRepositoriesHolder, RepositoriesHolder>();
	        services.AddScoped<IStockItemRepository, StockItemRepository>();
	        services.AddScoped<IDeliveryRequestRepository, DeliveryRequestRepository>();
        }

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseEndpoints(endpoints => endpoints.MapGrpcService<StockApiGrpService>());
		}
	}
}