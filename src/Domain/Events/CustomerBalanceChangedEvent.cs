using CustomerAccount.Domain.Common;
using CustomerAccount.Domain.Entities;

namespace CustomerAccount.Domain.Events;
public class CustomerBalanceChangedEvent : BaseEvent
{
    public CustomerBalanceChangedEvent(Customer customer)
    {
        Customer = customer;
    }

    public Customer Customer { get; }
}
