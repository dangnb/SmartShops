using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;
using Shop.Persistence.Constants;

namespace Shop.Persistence.Configurations;
internal sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable(TableNames.Companies);
        builder.HasKey(x => x.Id);
        builder.Property(t => t.Name).HasMaxLength(200).IsRequired(true);
        builder.Property(t => t.Code).HasMaxLength(50).IsRequired(true);
        builder.Property(t => t.Addess).HasMaxLength(200);
        builder.Property(t => t.Phone).HasMaxLength(20);
        builder.Property(t => t.Mail).HasMaxLength(50);
    }
}
