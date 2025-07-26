using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;
using Shop.Persistence.Constants;

namespace Shop.Persistence.Configurations;
public class ConfigConfiguration : IEntityTypeConfiguration<Config>
{
    public void Configure(EntityTypeBuilder<Config> builder)
    {
        builder.ToTable(TableNames.Configs);
        builder.HasKey(x => x.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();
        builder.Property(t => t.Code).HasMaxLength(50).IsRequired(true);
        builder.Property(t => t.Value).HasMaxLength(500).IsRequired(true);
    }
}
