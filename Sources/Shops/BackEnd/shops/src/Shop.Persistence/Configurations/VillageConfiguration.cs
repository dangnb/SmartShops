using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities.Metadata;
using Shop.Persistence.Constants;

namespace Shop.Persistence.Configurations;
public class VillageConfiguration : IEntityTypeConfiguration<Village>
{
    public void Configure(EntityTypeBuilder<Village> builder)
    {
        builder.ToTable(TableNames.Villages);
        builder.HasKey(x => x.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();
        builder.Property(t => t.Name).HasMaxLength(200).IsRequired(true);
        builder.Property(t => t.Code).HasMaxLength(50).IsRequired(true);
        builder.HasOne(t => t.Ward);
    }
}
