using Shop.Domain.Entities.Identity;
using Shop.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shop.Persistence.Configurations;
public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.ToTable(TableNames.AppRoles);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired(true);

        builder.Property(x => x.Description)
            .HasMaxLength(250)
            .IsRequired(true);

        //Each user can have many RoleClaim
        builder.HasMany(e => e.Claims)
            .WithOne()
            .HasForeignKey(uc => uc.RoleId)
            .IsRequired(true);

        //Earch User can have many entries in the UserRole join table
        builder.HasMany(x => x.UserRoles)
            .WithOne()
            .HasForeignKey(uc => uc.RoleId)
            .IsRequired(true);

        builder
            .HasMany(e => e.Permissions)
            .WithMany(c=>c.AppRoles);

    }
}
