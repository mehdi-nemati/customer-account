using Confluent.Kafka;
using CustomerAccount.Application.Common.EventBus;
using CustomerAccount.Domain.Common;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace CustomerAccount.EventBus.Kafka;
public class EventProducer : IEventProducer
{
    private readonly ProducerConfig _config;

    public EventProducer(IOptions<ProducerConfig> config)
    {
        _config = config.Value;
    }

    public async Task ProduceAsync<T>(string topic, T @event) where T : BaseEvent
    {
        using var producer = new ProducerBuilder<string, string>(_config)
            .SetKeySerializer(Serializers.Utf8)
            .SetValueSerializer(Serializers.Utf8)
            .Build();

        var headers = new Headers
            {
                {"type", Encoding.UTF8.GetBytes(@event.GetType().AssemblyQualifiedName)}
            };

        var eventMessage = new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = JsonSerializer.Serialize(@event, @event.GetType()),
            Headers = headers
        };

        var deliveryResult = await producer.ProduceAsync(topic, eventMessage);

        if (deliveryResult.Status == PersistenceStatus.NotPersisted)
        {
            throw new Exception($"Could not produce {@event.GetType().Name} message to topic - {topic} due to the following reason: {deliveryResult.Message}.");
        }
    }
}

