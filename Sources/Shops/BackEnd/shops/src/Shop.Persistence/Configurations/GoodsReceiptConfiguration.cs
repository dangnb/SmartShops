using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities.Purchases;

namespace Shop.Persistence.Configurations;

public sealed class GoodsReceiptConfiguration : IEntityTypeConfiguration<GoodsReceipt>
{
    public void Configure(EntityTypeBuilder<GoodsReceipt> b)
    {
        b.ToTable("goods_receipts");

        b.HasKey(x => x.Id);

        b.Property(x => x.ReceiptNo)
            .HasMaxLength(50)
            .IsRequired();

        b.HasIndex(x => x.ReceiptNo).IsUnique();

        b.Property(x => x.Status)
            .HasConversion<int>();

        b.Property(x => x.ReceiptDate)
            .HasColumnType("date");

        b.HasMany(x => x.Lines)
            .WithOne()
            .HasForeignKey("GoodsReceiptId")
            .OnDelete(DeleteBehavior.Cascade);

        // 🔥 DÒNG QUYẾT ĐỊNH SỐ PHẬN
        b.Navigation(x => x.Lines)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
