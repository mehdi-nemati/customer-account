using CustomerAccount.Application.Common.EventBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EventBus.Kafka;

public class ConsumerHostedService : IHostedService
{
    private readonly ILogger<ConsumerHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptions<EventBusTopic> _ClientOptions;

    public ConsumerHostedService(ILogger<ConsumerHostedService> logger,
        IServiceProvider serviceProvider,
        IOptions<EventBusTopic> clientOptions)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _ClientOptions = clientOptions;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event Consumer Service running.");

        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            var eventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();

            Task.Run(() => eventConsumer.Consume(_ClientOptions.Value.TopicName), cancellationToken);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event Consumer Service Stopped");

        return Task.CompletedTask;
    }
}
