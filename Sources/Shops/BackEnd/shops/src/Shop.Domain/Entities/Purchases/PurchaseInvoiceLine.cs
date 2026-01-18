public class PurchaseInvoiceLine
{
    protected PurchaseInvoiceLine() { }

    public Guid Id { get; protected set; } = Guid.NewGuid();
    public Guid PurchaseInvoiceId { get; protected set; }
    public Guid ProductId { get; protected set; }
    public decimal Qty { get; protected set; }
    public decimal UnitPrice { get; protected set; }
    public decimal TaxRate { get; protected set; }

    public decimal LineSubtotal => Qty * UnitPrice;
    public decimal LineTax => LineSubtotal * TaxRate;

    internal PurchaseInvoiceLine(Guid productId, decimal qty, decimal unitPrice, decimal taxRate)
    {
        ProductId = productId;
        Qty = qty;
        UnitPrice = unitPrice;
        TaxRate = taxRate;
    }
}
