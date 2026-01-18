using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Purchasing;

namespace Shop.Infrastructure.Persistence.Configurations.Purchasing;

public sealed class PurchaseReturnConfiguration
    : IEntityTypeConfiguration<PurchaseReturn>
{
    public void Configure(EntityTypeBuilder<PurchaseReturn> b)
    {
        b.ToTable("purchase_returns");

        // =========================
        // Key
        // =========================
        b.HasKey(x => x.Id);

        // =========================
        // Properties
        // =========================
        b.Property(x => x.ReturnNo)
            .HasMaxLength(50)
            .IsRequired();

        b.HasIndex(x => x.ReturnNo)
            .IsUnique();

        b.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        b.Property(x => x.ReturnDate)
            .HasColumnType("date")
            .IsRequired();

        b.Property(x => x.SupplierId)
            .IsRequired();

        b.Property(x => x.WarehouseId)
            .IsRequired();

        // =========================
        // Lines (BACKING FIELD)
        // =========================
        b.HasMany(x => x.Lines)
                 .WithOne()
                 .HasForeignKey("PurchaseReturnId")
                 .OnDelete(DeleteBehavior.Cascade);

        b.Navigation(x => x.Lines)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasField("_lines");

        // =========================
        // Audit (nếu có trong EntityAuditBase)
        // =========================
        // b.Property(x => x.CreatedAt);
        // b.Property(x => x.CreatedBy);
        // b.Property(x => x.UpdatedAt);
        // b.Property(x => x.UpdatedBy);
    }
}
