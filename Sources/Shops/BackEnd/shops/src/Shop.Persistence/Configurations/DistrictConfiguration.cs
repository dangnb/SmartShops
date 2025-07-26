using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities.Metadata;
using Shop.Persistence.Constants;

namespace Shop.Persistence.Configurations;
internal class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.ToTable(TableNames.Districts);
        builder.HasKey(x => x.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();
        builder.Property(t => t.CityId).IsRequired(true);
        builder.Property(t => t.Name).HasMaxLength(200).IsRequired(true);
        builder.Property(t => t.Code).HasMaxLength(50).IsRequired(true);
        builder.HasOne(t=>t.City);
    }
}
