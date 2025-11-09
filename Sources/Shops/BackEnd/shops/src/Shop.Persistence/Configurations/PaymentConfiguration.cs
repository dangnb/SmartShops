using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;
using Shop.Persistence.Constants;

namespace Shop.Persistence.Configurations;
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable(TableNames.Payments);
        builder.HasKey(x => x.Id);
    }
}
