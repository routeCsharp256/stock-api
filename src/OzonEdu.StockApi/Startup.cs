using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.StockApi.GrpcServices;
using OzonEdu.StockApi.Infrastructure.Extensions;
using OzonEdu.StockApi.Infrastructure.Middlewares;
using OzonEdu.StockApi.Services;
using OzonEdu.StockApi.Services.Interfaces;

namespace OzonEdu.StockApi
{
	public class Startup
	{
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }
        
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }
        
		public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IStockService, StockService>();
            services.AddInfrastructureServices();

            services.AddGrpc(options => options.Interceptors.Add<LoggingInterceptor>());
        }

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
			app.UseRouting();
			app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<StockApiGrpService>();
                endpoints.MapControllers();
            });
		}
	}
}