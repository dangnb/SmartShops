using Shop.Domain.Abstractions;
using Shop.Domain.Entities.enums;
using Shop.Domain.Entities.Purchases;
using Shop.Domain.Exceptions;

public class PurchaseInvoice : EntityAuditBase<Guid>
{
    protected PurchaseInvoice() { }

    private readonly List<PurchaseInvoiceLine> _lines = new();
    private readonly List<InvoiceReceiptMatch> _matches = new();

    public Guid SupplierId { get; protected set; }
    public string InvoiceNo { get; protected set; } = default!;
    public DateOnly InvoiceDate { get; protected set; }
    public DateOnly? DueDate { get; protected set; }
    public DocumentStatus Status { get; protected set; } = DocumentStatus.Draft;

    public decimal Subtotal { get; protected set; }
    public decimal Tax { get; protected set; }
    public decimal Total { get; protected set; }

    // 🔥 navigation MUST be virtual
    public virtual IReadOnlyCollection<PurchaseInvoiceLine> Lines => _lines;
    public virtual IReadOnlyCollection<InvoiceReceiptMatch> Matches => _matches;

    public PurchaseInvoice(Guid supplierId, string invoiceNo, DateOnly invoiceDate, DateOnly? dueDate)
    {
        if (string.IsNullOrWhiteSpace(invoiceNo))
            throw new DomainBaseException("InvoiceNo is required.");

        SupplierId = supplierId;
        InvoiceNo = invoiceNo.Trim();
        InvoiceDate = invoiceDate;
        DueDate = dueDate;
    }

    public void AddLine(Guid productId, decimal qty, decimal unitPrice, decimal taxRate = 0m)
    {
        EnsureDraft();

        if (qty <= 0)
            throw new DomainBaseException("Qty must be > 0.");
        if (unitPrice < 0)
            throw new DomainBaseException("UnitPrice must be >= 0.");
        if (taxRate < 0)
            throw new DomainBaseException("TaxRate must be >= 0.");

        _lines.Add(new PurchaseInvoiceLine(productId, qty, unitPrice, taxRate));
        Recalc();
    }

    public void Issue()
    {
        EnsureDraft();
        if (_lines.Count == 0)
            throw new DomainBaseException("Invoice must have lines.");

        Status = DocumentStatus.Issued;
    }

    public void AddMatch(Guid receiptId, Guid? receiptLineId, Guid productId, decimal qtyMatched, decimal amountMatched)
    {
        if (Status != DocumentStatus.Issued)
            throw new DomainBaseException("Invoice must be Issued before matching.");

        if (qtyMatched <= 0)
            throw new DomainBaseException("QtyMatched must be > 0.");

        _matches.Add(new InvoiceReceiptMatch(receiptId, receiptLineId, productId, qtyMatched, amountMatched));
    }

    protected void EnsureDraft()
    {
        if (Status != DocumentStatus.Draft)
            throw new DomainBaseException("Only Draft invoice can be modified.");
    }

    protected void Recalc()
    {
        Subtotal = _lines.Sum(x => x.LineSubtotal);
        Tax = _lines.Sum(x => x.LineTax);
        Total = Subtotal + Tax;
    }
}
