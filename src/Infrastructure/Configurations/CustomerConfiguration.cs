using CustomerAccount.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerAccount.Infrastructure.Configurations;

public class TodoItemConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(t => t.FullName)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(t => t.NationalCode)
            .HasMaxLength(10)
            .IsRequired();
    }
}
