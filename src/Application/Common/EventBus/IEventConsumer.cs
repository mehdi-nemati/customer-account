namespace CustomerAccount.Application.Common.EventBus;
public interface IEventConsumer
{
    Task Consume(string topic);
}