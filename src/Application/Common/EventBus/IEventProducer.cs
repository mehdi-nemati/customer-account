using CustomerAccount.Domain.Common;

namespace CustomerAccount.Application.Common.EventBus;
public interface IEventProducer
{
    Task ProduceAsync<T>(string topic, T @event) where T : BaseEvent;
}
