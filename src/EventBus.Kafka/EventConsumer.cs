using Confluent.Kafka;
using CustomerAccount.Application.Common.EventBus;
using CustomerAccount.Application.Common.Interfaces;
using CustomerAccount.Domain.Common;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace CustomerAccount.EventBus.Kafka;
public class EventConsumer : IEventConsumer
{
    private readonly ConsumerConfig _config;
    private readonly IEventInvoke _eventInvoke;

    public EventConsumer(IOptions<ConsumerConfig> config, IEventInvoke eventHandler)
    {
        _config = config.Value;
        _eventInvoke = eventHandler;
    }

    public async Task Consume(string topic)
    {
        using var consumer = new ConsumerBuilder<string, string>(_config)
                .SetKeyDeserializer(Deserializers.Utf8)
                .SetValueDeserializer(Deserializers.Utf8)
                .Build();

        consumer.Subscribe(topic);

        while (true)
        {
            var consumeResult = consumer.Consume();

            if (consumeResult?.Message == null) continue;

            var messageTypeHeader = consumeResult.Message.Headers.First(h => h.Key == "type");
            var eventTypeName = Encoding.UTF8.GetString(messageTypeHeader.GetValueBytes());
            var eventType = Type.GetType(eventTypeName);

            var @event = JsonSerializer.Deserialize(consumeResult.Message.Value, eventType) as BaseEvent;

            var handlerMethod = _eventInvoke.GetType().GetMethod("On", new Type[] { @event.GetType() });

            if (handlerMethod == null)
            {
                throw new ArgumentNullException(nameof(handlerMethod), "Could not find event handler method!");
            }

            handlerMethod.Invoke(_eventInvoke, new object[] { @event });
            consumer.Commit(consumeResult);
        }
    }
}

