using Confluent.Kafka;
using CustomerAccount.Application.Common.EventBus;
using CustomerAccount.EventBus.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Kafka;

public static class ConfigureServices
{
    public static IServiceCollection AddKafkaServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EventBusTopic>(configuration.GetSection(nameof(EventBusTopic)));

        services.Configure<ProducerConfig>(configuration.GetSection(nameof(ProducerConfig)));
        services.Configure<ConsumerConfig>(configuration.GetSection(nameof(ConsumerConfig)));

        services.AddScoped<IEventConsumer, EventConsumer>();
        services.AddScoped<IEventProducer, EventProducer>();

        services.AddHostedService<ConsumerHostedService>();

        return services;
    }
}