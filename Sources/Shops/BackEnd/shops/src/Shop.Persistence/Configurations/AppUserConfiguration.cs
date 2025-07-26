using Shop.Domain.Entities.Identity;
using Shop.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;

namespace Shop.Persistence.Configurations;
public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable(TableNames.AppUsers);
        builder.HasKey(x => x.Id);
        //Earch User can have many entries in the UserRole join table
        builder.HasMany(x => x.Roles)
            .WithOne()
            .HasForeignKey(uc => uc.UserId)
            .IsRequired(true);
        builder.HasMany(x => x.Districts)
          .WithOne()
          .HasForeignKey(uc => uc.UserId)
          .IsRequired(true);
    }
}

public class AppUserDistrictConfiguration : IEntityTypeConfiguration<AppUserDistrict>
{
    public void Configure(EntityTypeBuilder<AppUserDistrict> builder)
    {
        builder.ToTable(TableNames.AppUserDistricts);

        builder.HasKey(x => new { x.DistrictId, x.UserId });
    }
}
