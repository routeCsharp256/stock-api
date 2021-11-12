using System;
using System.IO;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OzonEdu.StockApi.Infrastructure.Filters;
using OzonEdu.StockApi.Infrastructure.Interceptors;
using OzonEdu.StockApi.Infrastructure.StartupFilters;

namespace OzonEdu.StockApi.Infrastructure.Extensions
{
    internal static class HostBuilderExtensions
    {
        internal static IHostBuilder ConfigureMicroserviceInfrastructure(this IHostBuilder builder)
        {
            return builder
                .ConfigureVersionEndpoint()
                .ConfigureHttp()
                .ConfigureGrpc()
                .ConfigureSwagger();
        }

        private static IHostBuilder ConfigureVersionEndpoint(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter, VersionStartupFilter>();
            });

            return builder;
        }
        
        private static IHostBuilder ConfigureSwagger(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "OzonEdu.StockApi", Version = "v1"});
                    options.CustomSchemaIds(x => x.FullName);
                    var xmlFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                    var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                    options.IncludeXmlComments(xmlFilePath);
                });
            });
            
            return builder;
        }
        
        private static IHostBuilder ConfigureHttp(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());
                services.AddSingleton<IStartupFilter, HttpStartupFilter>();
            });

            return builder;
        }

        private static IHostBuilder ConfigureGrpc(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddGrpc(options => options.Interceptors.Add<LoggingInterceptor>());
            });

            return builder;
        }
        
        public static IHostBuilder ConfigurePorts(this IHostBuilder builder)
        {
            var httpPortEnv = Environment.GetEnvironmentVariable("HTTP_PORT");
            if (!int.TryParse(httpPortEnv, out var httpPort))
            {
                httpPort = 5000;
            }

            var grpcPortEnv = Environment.GetEnvironmentVariable("GRPC_PORT");
            if (!int.TryParse(grpcPortEnv, out var grpcPort))
            {
                grpcPort = 5002;
            }
            builder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.ConfigureKestrel(
                    options =>
                    {
                        Listen(options, httpPort, HttpProtocols.Http1);
                        Listen(options, grpcPort, HttpProtocols.Http2);
                    });
            });
            return builder;
        }

        static void Listen(KestrelServerOptions kestrelServerOptions, int? port, HttpProtocols protocols)
        {
            if (port == null)
                return;

            var address = IPAddress.Any;


            kestrelServerOptions.Listen(address, port.Value, listenOptions => { listenOptions.Protocols = protocols; });
        }
    }
}