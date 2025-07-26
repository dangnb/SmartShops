using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities.Identity;
using Shop.Persistence.Constants;

namespace Shop.Persistence.Configurations;
internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permissions);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).IsRequired();
        builder.Property(x => x.Description).IsRequired();
    }
}
