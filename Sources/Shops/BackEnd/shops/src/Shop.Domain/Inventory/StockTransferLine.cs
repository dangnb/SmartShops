namespace Shop.Domain.Inventory;

public class StockTransferLine 
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid StockTransferId { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal Qty { get; private set; }

    protected StockTransferLine() { } // ✅ cho EF

    internal StockTransferLine(Guid productId, decimal qty)
    {
        ProductId = productId;
        Qty = qty;
    }
}
