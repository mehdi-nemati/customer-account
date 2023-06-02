using CustomerAccount.Application.Common.Mapping;
using CustomerAccount.Domain.Entities;

namespace CustomerAccount.Application.Customers.Queries.Customers;

public class GetCustomerDto : IMapFrom<Customer>
{
    public int Id { get; init; }

    public string? FullName { get; set; }

    public string? NationalCode { get; set; }

    public int WalletBalance { get; set; }
}
