using Shop.Domain.Abstractions;
using Shop.Domain.Entities.enums;
using Shop.Domain.Exceptions;

namespace Shop.Domain.Entities.Purchases;

public class GoodsReceipt : EntityAuditBase<Guid>
{
    // EF Core sẽ tự dùng backing field ngầm
    protected GoodsReceipt() { }

    private readonly List<GoodsReceiptLine> _lines = new();

    public string ReceiptNo { get; private set; } = default!;
    public Guid SupplierId { get; private set; }
    public Guid WarehouseId { get; private set; }
    public DateOnly ReceiptDate { get; private set; }
    public DocumentStatus Status { get; private set; } = DocumentStatus.Draft;

    public decimal Subtotal { get; private set; }
    public decimal Total { get; private set; }

    // ✅ NAVIGATION DUY NHẤT – PHẢI VIRTUAL
    public virtual IReadOnlyCollection<GoodsReceiptLine> Lines => _lines;

    public GoodsReceipt(
        string receiptNo,
        Guid supplierId,
        Guid warehouseId,
        DateOnly receiptDate)
    {
        if (string.IsNullOrWhiteSpace(receiptNo))
            throw new DomainBaseException("ReceiptNo is required.");

        ReceiptNo = receiptNo.Trim();
        SupplierId = supplierId;
        WarehouseId = warehouseId;
        ReceiptDate = receiptDate;
    }

    public void AddLine(Guid productId, decimal qty, decimal? unitCost)
    {
        EnsureDraft();

        if (qty <= 0)
            throw new DomainBaseException("Qty must be > 0.");

        _lines.Add(new GoodsReceiptLine(productId, qty, unitCost));
        Recalc();
    }

    public void Post(DateTime utcNow)
    {
        EnsureDraft();

        if (_lines.Count == 0)
            throw new DomainBaseException("Cannot post receipt without lines.");

        Status = DocumentStatus.Posted;

        Raise(new GoodsReceiptPostedEvent(
            Id,
            SupplierId,
            WarehouseId,
            utcNow,
            _lines.Select(l =>
                new GoodsReceiptPostedLine(l.ProductId, l.Qty, l.UnitCost)
            ).ToList()
        ));
    }

    private void EnsureDraft()
    {
        if (Status != DocumentStatus.Draft)
            throw new DomainBaseException("Only Draft receipt can be modified.");
    }

    private void Recalc()
    {
        Subtotal = _lines.Sum(x => (x.UnitCost ?? 0m) * x.Qty);
        Total = Subtotal;
    }
}

