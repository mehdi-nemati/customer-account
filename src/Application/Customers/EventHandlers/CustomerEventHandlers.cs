using CustomerAccount.Application.Common.Interfaces;
using CustomerAccount.Domain.Common;
using CustomerAccount.Domain.Events;

namespace CustomerAccount.Application.Customers.EventHandlers;
public class CustomerEventHandlers : IEventInvoke, ICustomerEventHandlers
{
    private readonly IEventStoreRepository _eventStore;
    public CustomerEventHandlers(IEventStoreRepository eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task On(CustomerCreatedEvent @event)
    {
        var eventModel = new EventModel
        {
            TimeStamp = DateTime.Now,
            AggregateIdentifier = @event.Customer.Id,
            EventType = @event.GetType().Name,
            EventData = @event
        };

        await _eventStore.SaveAsync(eventModel);
    }

    public async Task On(CustomerBalanceChangedEvent @event)
    {
        var eventModel = new EventModel
        {
            TimeStamp = DateTime.Now,
            AggregateIdentifier = @event.Customer.Id,
            EventType = @event.GetType().Name,
            EventData = @event
        };

        await _eventStore.SaveAsync(eventModel);
    }
}
