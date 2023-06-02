using CustomerAccount.Domain.Common;

namespace CustomerAccount.Domain.Entities;
public class Customer : BaseEntity
{
    public string? FullName { get; set; }

    public string? Address { get; set; }

    public string? NationalCode { get; set; }

    public int WalletBalance { get; set; }
}
