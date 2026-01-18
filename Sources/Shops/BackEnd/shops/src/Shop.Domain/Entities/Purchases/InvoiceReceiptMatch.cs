public class InvoiceReceiptMatch
{
    protected InvoiceReceiptMatch() { }

    public Guid Id { get; protected set; } = Guid.NewGuid();
    public Guid PurchaseInvoiceId { get; protected set; }
    public Guid ReceiptId { get; protected set; }
    public Guid? ReceiptLineId { get; protected set; }
    public Guid ProductId { get; protected set; }
    public decimal QtyMatched { get; protected set; }
    public decimal AmountMatched { get; protected set; }

    internal InvoiceReceiptMatch(
        Guid receiptId,
        Guid? receiptLineId,
        Guid productId,
        decimal qtyMatched,
        decimal amountMatched)
    {
        ReceiptId = receiptId;
        ReceiptLineId = receiptLineId;
        ProductId = productId;
        QtyMatched = qtyMatched;
        AmountMatched = amountMatched;
    }
}
