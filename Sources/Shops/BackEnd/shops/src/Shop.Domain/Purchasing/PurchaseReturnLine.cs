namespace Shop.Domain.Purchasing;

public class PurchaseReturnLine   
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid PurchaseReturnId { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal Qty { get; private set; }

    protected PurchaseReturnLine() { } // ❗ cho proxy

    internal PurchaseReturnLine(Guid productId, decimal qty)
    {
        ProductId = productId;
        Qty = qty;
    }
}
