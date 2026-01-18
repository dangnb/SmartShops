using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Inventory;

public class InventoryAdjustmentConfiguration
    : IEntityTypeConfiguration<InventoryAdjustment>
{
    public void Configure(EntityTypeBuilder<InventoryAdjustment> b)
    {
        b.ToTable("inventory_adjustments");

        b.HasKey(x => x.Id);

        b.Property(x => x.AdjustmentNo)
            .HasMaxLength(50)
            .IsRequired();

        b.Property(x => x.Status)
            .HasConversion<int>();

        b.Property(x => x.AdjustmentDate)
            .HasColumnType("date");

        // =========================
        // Lines (BACKING FIELD)
        // =========================
        b.HasMany(x => x.Lines)
            .WithOne()
            .HasForeignKey("InventoryAdjustmentId")
            .OnDelete(DeleteBehavior.Cascade);

        b.Navigation(x => x.Lines)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasField("_lines");
    }
}
