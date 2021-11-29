using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using OzonEdu.StockApi;
using OzonEdu.StockApi.Infrastructure.Extensions;
using Serilog;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args)
    => Host.CreateDefaultBuilder(args)
        .UseSerilog((context, configuration) => configuration
                .ReadFrom
                .Configuration(context.Configuration)
                .WriteTo.Console())
        .ConfigurePorts()
        .ConfigureMicroserviceInfrastructure();