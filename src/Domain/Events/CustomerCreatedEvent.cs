using CustomerAccount.Domain.Common;
using CustomerAccount.Domain.Entities;

namespace CustomerAccount.Domain.Events;

public class CustomerCreatedEvent : BaseEvent
{
    public CustomerCreatedEvent(Customer customer)
    {
        Customer = customer;
    }

    public Customer Customer { get; }
}
