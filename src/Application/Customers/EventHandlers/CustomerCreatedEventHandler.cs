using CustomerAccount.Application.Common.EventBus;
using CustomerAccount.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CustomerAccount.Application.Customers.EventHandlers;

public class CustomerCreatedEventHandler : INotificationHandler<CustomerCreatedEvent>
{
    private readonly IOptions<EventBusTopic> _ClientOptions;
    private readonly IEventProducer _eventProducerProducer;
    private readonly ILogger<CustomerCreatedEventHandler> _logger;

    public CustomerCreatedEventHandler(ILogger<CustomerCreatedEventHandler> logger, IEventProducer eventProducerProducer, IOptions<EventBusTopic> clientOptions)
    {
        _logger = logger;
        _eventProducerProducer = eventProducerProducer;
        _ClientOptions = clientOptions;
    }

    public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _eventProducerProducer.ProduceAsync(_ClientOptions.Value.TopicName, notification);

        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);
    }
}
