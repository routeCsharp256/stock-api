using System;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace OzonEdu.StockApi.Infrastructure.Interceptors
{
    internal class LoggingInterceptor : Interceptor
    {
        private readonly ILogger<LoggingInterceptor> _logger;
        private readonly JsonSerializerOptions _defaultSerializationOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public LoggingInterceptor(ILogger<LoggingInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                _logger.LogInformation($"Grpc request {context.Method}");
                var requestJson = JsonSerializer.Serialize(request, _defaultSerializationOptions);
                _logger.LogInformation(requestJson);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Could not log grpc request");
            }

            var response = await base.UnaryServerHandler(request, context, continuation);

            try
            {
                var responseJson = JsonSerializer.Serialize(response, _defaultSerializationOptions);
                _logger.LogInformation(responseJson);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Could not log grpc response");
            }

            return response;
        }
    }
}