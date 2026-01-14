namespace Shop.Domain.Entities.Purchases;

public class GoodsReceiptLine
{
    public Guid Id { get; private set; }
    public Guid GoodsReceiptId { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal Qty { get; private set; }
    public decimal? UnitCost { get; private set; }

    public GoodsReceiptLine(Guid productId, decimal qty, decimal? unitCost = null)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Qty = qty;
        UnitCost = unitCost;
    }
}
