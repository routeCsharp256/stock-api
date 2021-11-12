using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OzonEdu.StockApi.Infrastructure.Extensions;

namespace OzonEdu.StockApi.Infrastructure.Middlewares
{
    internal class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (string.Equals(context.Request.ContentType, "application/grpc", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }
            
            await LogRequest(context);
            await _next(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            try
            {
                _logger.LogInformation($"Http request {context.Request.Path}");
                if (context.Request.ContentLength > 0)
                {
                    context.Request.EnableBuffering();
                
                    var buffer = new byte[context.Request.ContentLength.Value];
                    await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
                    var bodyAsText = Encoding.UTF8.GetString(buffer);
                    var serializedJsonOutput = JsonSerializer.Serialize(bodyAsText, JsonSerializerOptionsFactory.Default);
                    _logger.LogInformation("Request logged");
                    _logger.LogInformation(serializedJsonOutput);

                    context.Request.Body.Position = 0;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Could not log request body");
            }
        }
    }
}