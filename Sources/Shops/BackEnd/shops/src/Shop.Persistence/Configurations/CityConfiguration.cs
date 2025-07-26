using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Persistence.Constants;
using Shop.Domain.Entities.Metadata;

namespace Shop.Persistence.Configurations;
internal class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable(TableNames.Cities);
        builder.HasKey(x => x.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();
        builder.Property(t => t.Name).HasMaxLength(200).IsRequired(true);
        builder.Property(t => t.Code).HasMaxLength(50).IsRequired(true);
    }
}

