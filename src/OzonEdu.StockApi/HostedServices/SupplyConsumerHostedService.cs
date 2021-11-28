using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using CSharpCourse.Core.Lib.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OzonEdu.StockApi.Infrastructure.Commands.ReplenishStock;
using OzonEdu.StockApi.Infrastructure.Configuration;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.HostedServices
{
    public class SupplyConsumerHostedService : BackgroundService
    {
        private readonly KafkaConfiguration _config;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger _logger;

        protected string Topic { get; set; } = "supply_ship_event";
        
        protected SupplyConsumerHostedService(
            IOptions<KafkaConfiguration> config,
            IServiceScopeFactory scopeFactory,
            ILogger logger, IMediator mediator)
        {
            _config = config.Value;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = _config.GroupId,
                BootstrapServers = _config.BootstrapServers,
            };

            using (var c = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                c.Subscribe(Topic);
                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            try
                            {
                                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                                var cr = c.Consume(stoppingToken);
                                if (cr != null)
                                {
                                    var message = JsonSerializer.Deserialize<SupplyShippedEvent>(cr.Message.Value);
                                    await mediator.Send(new ReplenishStockCommand()
                                    {
                                        SupplyId = message.SupplyId,
                                        Items = message.Items.Select(it => new StockItemQuantityDto()
                                        {
                                            Quantity = (int)it.Quantity,
                                            Sku = it.SkuId
                                        }).ToArray()
                                    }, stoppingToken);
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError($"Error: {ex.Message}");
                                throw;
                            }
                        }
                    }
                }
                finally
                {
                    c.Close();
                }
            }
        }
    }
}