using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities.Inventory;

public class StockTransferConfiguration
    : IEntityTypeConfiguration<StockTransfer>
{
    public void Configure(EntityTypeBuilder<StockTransfer> b)
    {
        b.ToTable("stock_transfers");

        b.HasKey(x => x.Id);

        b.Property(x => x.TransferNo)
            .HasMaxLength(50)
            .IsRequired();

        b.Property(x => x.Status)
            .HasConversion<int>();

        b.Property(x => x.TransferDate)
            .HasColumnType("date");

        // =========================
        // Lines (BACKING FIELD)
        // =========================
        b.HasMany(x => x.Lines)
            .WithOne()
            .HasForeignKey("StockTransferId")
            .OnDelete(DeleteBehavior.Cascade);

        b.Navigation(x => x.Lines)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasField("_lines");
    }
}
