using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Persistence.Constants;

namespace Shop.Persistence.Configurations;
internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(TableNames.Customers);
        builder.HasKey(x => x.Id);
        builder.Property(t => t.Name).HasMaxLength(200).IsRequired(true);
        builder.Property(t => t.Code).HasMaxLength(50).IsRequired(true);

    }
}
