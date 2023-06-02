using CustomerAccount.Application.Common.Interfaces;
using CustomerAccount.Domain.Entities;
using CustomerAccount.Domain.Events;
using MediatR;

namespace CustomerAccount.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand : IRequest<int>
{
    public string? FullName { get; set; }

    public string? Address { get; set; }

    public string? NationalCode { get; set; }

    public int WalletBalance { get; set; }
}

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = new Customer
        {
            NationalCode = request.NationalCode,
            Address = request.Address,
            FullName = request.FullName,
            WalletBalance = request.WalletBalance,
        };

        entity.AddDomainEvent(new CustomerCreatedEvent(entity));

        await _context.Customers.AddAsync(entity, cancellationToken);
        await _context.SaveChangesThenPublishEventAsync(cancellationToken);

        return entity.Id;
    }
}
