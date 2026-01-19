using Shop.Domain.Abstractions;

namespace Shop.Domain.Entities.Purchases;

public class GoodsReceiptLine : DomainEntity<Guid>
{
    protected GoodsReceiptLine() { }
    public Guid GoodsReceiptId { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal Qty { get; private set; }
    public decimal? UnitCost { get; private set; }

    internal GoodsReceiptLine(Guid receiptId, Guid productId, decimal qty, decimal? unitCost)
    {
        GoodsReceiptId = receiptId;
        ProductId = productId;
        Qty = qty;
        UnitCost = unitCost;
    }

    public void Update(decimal qty, decimal? unitCost)
    {
        Qty = qty;
        UnitCost = unitCost;
    }
}
