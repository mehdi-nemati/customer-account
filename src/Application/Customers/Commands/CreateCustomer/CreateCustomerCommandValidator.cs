using CustomerAccount.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateCustomerCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.FullName)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(v => v.NationalCode)
                .MaximumLength(10).NotEmpty()
                .MustAsync(BeUniqueTitle).WithMessage("The National code already exists."); ;

        }

       
        public async Task<bool> BeUniqueTitle(string NationalCode, CancellationToken cancellationToken)
        {
            return !await _context.Customers
                .AnyAsync(l => l.NationalCode == NationalCode, cancellationToken);
        }
    }
}
