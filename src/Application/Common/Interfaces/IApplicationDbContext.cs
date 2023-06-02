using CustomerAccount.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task<int> SaveChangesThenPublishEventAsync(CancellationToken cancellationToken);
}