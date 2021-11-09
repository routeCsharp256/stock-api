using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.StockApi.GrpcServices;

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
        {;
	        services.AddMediatR(typeof(Startup));
        }

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseEndpoints(endpoints => endpoints.MapGrpcService<StockApiGrpService>());
		}
	}
}