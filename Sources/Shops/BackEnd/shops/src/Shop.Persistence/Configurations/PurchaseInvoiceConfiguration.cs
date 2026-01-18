using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities.Purchases;

public sealed class PurchaseInvoiceConfiguration
    : IEntityTypeConfiguration<PurchaseInvoice>
{
    public void Configure(EntityTypeBuilder<PurchaseInvoice> b)
    {
        b.ToTable("purchase_invoices");

        b.HasKey(x => x.Id);

        b.Property(x => x.InvoiceNo)
            .HasMaxLength(50)
            .IsRequired();

        b.HasIndex(x => x.InvoiceNo).IsUnique();

        b.Property(x => x.Status)
            .HasConversion<int>();

        b.Property(x => x.InvoiceDate)
            .HasColumnType("date");

        b.Property(x => x.DueDate)
            .HasColumnType("date");

        // =========================
        // Lines (DDD + Lazy Loading)
        // =========================
        b.HasMany(x => x.Lines)
            .WithOne()
            .HasForeignKey("PurchaseInvoiceId")
            .OnDelete(DeleteBehavior.Cascade);

        b.Navigation(x => x.Lines)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasField("_lines");

        // =========================
        // Matches (DDD + Lazy Loading)
        // =========================
        b.HasMany(x => x.Matches)
            .WithOne()
            .HasForeignKey("PurchaseInvoiceId")
            .OnDelete(DeleteBehavior.Cascade);

        b.Navigation(x => x.Matches)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasField("_matches");

        // =========================
        // Money
        // =========================
        b.Property(x => x.Subtotal)
            .HasPrecision(18, 2);

        b.Property(x => x.Tax)
            .HasPrecision(18, 2);

        b.Property(x => x.Total)
            .HasPrecision(18, 2);
    }
}
