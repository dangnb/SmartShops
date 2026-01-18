namespace Shop.Domain.Inventory;

public class InventoryAdjustmentLine // ❌ bỏ sealed
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid InventoryAdjustmentId { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal QtyDelta { get; private set; }
    public decimal? UnitCost { get; private set; }

    protected InventoryAdjustmentLine() { } // ✅ cho EF

    internal InventoryAdjustmentLine(
        Guid productId,
        decimal qtyDelta,
        decimal? unitCost)
    {
        ProductId = productId;
        QtyDelta = qtyDelta;
        UnitCost = unitCost;
    }
}
