using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.StockApi.GrpcServices;
using OzonEdu.StockApi.Infrastructure.Middlewares;
using OzonEdu.StockApi.Services;
using OzonEdu.StockApi.Services.Interfaces;

namespace OzonEdu.StockApi
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IStockService, StockService>();

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