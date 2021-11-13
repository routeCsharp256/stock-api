using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using OzonEdu.StockApi;
using OzonEdu.StockApi.Infrastructure.Extensions;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args)
    => Host.CreateDefaultBuilder(args)
        .ConfigurePorts()
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
        .ConfigureMicroserviceInfrastructure();


