using Amazon.Runtime.Internal.Transform;
using CustomerAccount.Application.Common.Interfaces;
using CustomerAccount.Domain.Entities;
using Infrastructure.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CustomerAccount.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IMediator _mediator;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Customer> Customers => Set<Customer>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveChangesThenPublishEventAsync(CancellationToken cancellationToken)
        {
            var SaveToDbRes = await base.SaveChangesAsync(cancellationToken);

            if (SaveToDbRes > 0)
                await _mediator.DispatchDomainEvents(this);

            return SaveToDbRes;
        }
    }
}
