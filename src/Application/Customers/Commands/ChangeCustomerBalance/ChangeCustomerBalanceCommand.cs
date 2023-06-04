using CustomerAccount.Application.Common.Exceptions;
using CustomerAccount.Application.Common.Interfaces;
using CustomerAccount.Domain.Entities;
using CustomerAccount.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Application.Customers.Commands.ChangeCustomerBalance;

public record ChangeCustomerBalanceCommand : IRequest<int>
{
    public int UserId { get; set; }
    public int Count { get; set; }
}

public class ChangeCustomerBalanceHandler : IRequestHandler<ChangeCustomerBalanceCommand, int>
{
    private readonly IApplicationDbContext _context;

    public ChangeCustomerBalanceHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(ChangeCustomerBalanceCommand request, CancellationToken cancellationToken)
    {
        var FindedUser = await _context.Customers.FirstOrDefaultAsync(x => x.Id == request.UserId);
        if (FindedUser == null)
        {
            throw new NotFoundException(nameof(Customer), request.UserId);
        }

        FindedUser.WalletBalance += request.Count;

        FindedUser.AddDomainEvent(new CustomerBalanceChangedEvent(FindedUser));

        _context.Customers.Update(FindedUser);
        return await _context.SaveChangesThenPublishEventAsync(cancellationToken);
    }
}
