using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OzonEdu.StockApi.Infrastructure.Extensions;

namespace OzonEdu.StockApi.Infrastructure.Middlewares
{
    internal class ResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseLoggingMiddleware> _logger;

        internal ResponseLoggingMiddleware(RequestDelegate next, ILogger<ResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;
            
            await _next(context);

            try
            {
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);
            
                var serializedJsonOutput = JsonSerializer.Serialize(text, JsonSerializerOptionsFactory.Default);
                _logger.LogInformation("Request logged");
                _logger.LogInformation(serializedJsonOutput);
                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Could not log response");
            }
        }
    }
}