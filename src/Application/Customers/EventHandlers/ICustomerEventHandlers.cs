using CustomerAccount.Domain.Events;

namespace CustomerAccount.Application.Customers.EventHandlers;
public interface ICustomerEventHandlers 
{
    Task On(CustomerCreatedEvent @event);
    Task On(CustomerBalanceChangedEvent @event);
}
