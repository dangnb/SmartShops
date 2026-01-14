using Shop.Domain.Abstractions;
using Shop.Domain.Entities.enums;
using Shop.Domain.Exceptions;

namespace Shop.Domain.Entities.Purchases;

public sealed class PurchaseInvoice : EntityAuditBase<Guid>
{
    private readonly List<PurchaseInvoiceLine> _lines = new();
    private readonly List<InvoiceReceiptMatch> _matches = new();

    public Guid SupplierId { get; private set; }
    public string InvoiceNo { get; private set; } = default!;
    public DateOnly InvoiceDate { get; private set; }
    public DateOnly? DueDate { get; private set; }
    public DocumentStatus Status { get; private set; } = DocumentStatus.Draft;

    public decimal Subtotal { get; private set; }
    public decimal Tax { get; private set; }
    public decimal Total { get; private set; }

    public IReadOnlyCollection<PurchaseInvoiceLine> Lines => _lines.AsReadOnly();
    public IReadOnlyCollection<InvoiceReceiptMatch> Matches => _matches.AsReadOnly();

    private PurchaseInvoice() { }

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

    // Matching: chỉ kiểm rule “phải Issued”, còn kiểm qty remaining là ở Application (vì cần query chéo nhiều chứng từ)
    public void AddMatch(Guid receiptId, Guid? receiptLineId, Guid productId, decimal qtyMatched, decimal amountMatched)
    {
        if (Status != DocumentStatus.Issued)
            throw new DomainBaseException("Invoice must be Issued before matching.");
        if (qtyMatched <= 0)
            throw new DomainBaseException("QtyMatched must be > 0.");
        if (amountMatched < 0)
            throw new DomainBaseException("AmountMatched must be >= 0.");

        _matches.Add(new InvoiceReceiptMatch(receiptId, receiptLineId, productId, qtyMatched, amountMatched));
    }

    private void EnsureDraft()
    {
        if (Status != DocumentStatus.Draft)
            throw new DomainBaseException("Only Draft invoice can be modified.");
    }

    private void Recalc()
    {
        Subtotal = _lines.Sum(x => x.LineSubtotal);
        Tax = _lines.Sum(x => x.LineTax);
        Total = Subtotal + Tax;
    }
}

public sealed class PurchaseInvoiceLine
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid PurchaseInvoiceId { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal Qty { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TaxRate { get; private set; } // ví dụ 0.1 = 10%

    public decimal LineSubtotal => Qty * UnitPrice;
    public decimal LineTax => LineSubtotal * TaxRate;

    private PurchaseInvoiceLine() { }
    internal PurchaseInvoiceLine(Guid productId, decimal qty, decimal unitPrice, decimal taxRate)
    {
        ProductId = productId;
        Qty = qty;
        UnitPrice = unitPrice;
        TaxRate = taxRate;
    }
}

public sealed class InvoiceReceiptMatch
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid PurchaseInvoiceId { get; private set; }
    public Guid ReceiptId { get; private set; }
    public Guid? ReceiptLineId { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal QtyMatched { get; private set; }
    public decimal AmountMatched { get; private set; }

    private InvoiceReceiptMatch() { }
    internal InvoiceReceiptMatch(Guid receiptId, Guid? receiptLineId, Guid productId, decimal qtyMatched, decimal amountMatched)
    {
        ReceiptId = receiptId;
        ReceiptLineId = receiptLineId;
        ProductId = productId;
        QtyMatched = qtyMatched;
        AmountMatched = amountMatched;
    }
}
