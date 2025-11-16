using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Identity;
using Shop.Persistence.Constants;

namespace Shop.Persistence.Configurations;

internal class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable(TableNames.Suppliers);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.ShortName)
            .HasMaxLength(255);

        builder.Property(x => x.TaxCode)
            .HasMaxLength(50);

        builder.Property(x => x.Phone)
            .HasMaxLength(50);

        builder.Property(x => x.Email)
            .HasMaxLength(255);

        builder.Property(x => x.Website)
            .HasMaxLength(255);

        builder.Property(x => x.ContactName)
            .HasMaxLength(255);

        builder.Property(x => x.ContactPhone)
            .HasMaxLength(50);

        builder.Property(x => x.ContactEmail)
            .HasMaxLength(255);

        builder.Property(x => x.AddressLine)
            .HasMaxLength(255);

        builder.Property(x => x.FullAddress)
            .HasMaxLength(500);

        builder.Property(x => x.BankName)
            .HasMaxLength(255);

        builder.Property(x => x.BankAccountNo)
            .HasMaxLength(100);

        builder.Property(x => x.BankAccountName)
            .HasMaxLength(255);

        builder.Property(x => x.Note)
            .HasMaxLength(1000);

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);
    }
}
