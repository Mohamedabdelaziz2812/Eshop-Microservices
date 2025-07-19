using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        // but here the id is value object I we need to specify the type in Db , so we are gonna use the type conversion 
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(s => s.Value, // here for storing in Db
            dbId => CustomerId.Of(dbId)); // read from db order to reach it in opur application 


        builder.Property(c => c.Name).HasMaxLength(100).IsRequired();

        builder.Property(c => c.Email).HasMaxLength(255);

        builder.HasIndex(c => c.Email).IsUnique();
    }
}
