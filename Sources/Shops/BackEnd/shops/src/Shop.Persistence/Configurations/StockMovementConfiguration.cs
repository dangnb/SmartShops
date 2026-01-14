using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities.Purchases;

namespace Shop.Persistence.Configurations;

public sealed class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> b)
    {
        b.ToTable("stock_movements");
        b.HasKey(x => x.Id);
        b.Property(x => x.RefType).HasMaxLength(20).IsRequired();
        b.Property(x => x.QtyIn).HasPrecision(18, 3);
        b.Property(x => x.QtyOut).HasPrecision(18, 3);
        b.Property(x => x.UnitCost).HasPrecision(18, 2);

        b.HasIndex(x => new { x.WarehouseId, x.ProductId, x.MoveAtUtc });
    }
}
