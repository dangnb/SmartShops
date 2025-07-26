using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;
using Shop.Persistence.Constants;

namespace Shop.Persistence.Configurations;
public class PaymentSummaryConfiguration : IEntityTypeConfiguration<PaymentSummary>
{
    public void Configure(EntityTypeBuilder<PaymentSummary> builder)
    {
        builder.ToTable(TableNames.PaymentSummaries);
        builder.HasKey(x => x.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();
    }
}
