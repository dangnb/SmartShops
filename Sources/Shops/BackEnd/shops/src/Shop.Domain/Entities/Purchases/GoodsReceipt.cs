using Shop.Domain.Abstractions;
using Shop.Domain.Entities.enums;
using Shop.Domain.Exceptions;

namespace Shop.Domain.Entities.Purchases;

public class GoodsReceipt : EntityAuditBase<Guid>
{
    // _lines không phải là property trực tiếp ánh xạ qua EF Core
    private readonly List<GoodsReceiptLine> _lines = new();

    // Property công khai cho EF Core
    public string ReceiptNo { get; private set; } = default!;
    public Guid SupplierId { get; private set; }
    public Guid WarehouseId { get; private set; }
    public DateOnly ReceiptDate { get; private set; }
    public DocumentStatus Status { get; private set; } = DocumentStatus.Draft;

    public decimal Subtotal { get; private set; }
    public decimal Total { get; private set; }

    // Các navigation properties cần phải là `virtual` cho Lazy Loading
    public IReadOnlyCollection<GoodsReceiptLine> Lines => _lines.AsReadOnly();  // <-- Đây là property công khai

    // Constructor protected hoặc public
    protected GoodsReceipt() { }

    public GoodsReceipt(string receiptNo, Guid supplierId, Guid warehouseId, DateOnly receiptDate)
    {
        if (string.IsNullOrWhiteSpace(receiptNo))
            throw new DomainBaseException("ReceiptNo is required.");
        ReceiptNo = receiptNo.Trim();
        SupplierId = supplierId;
        WarehouseId = warehouseId;
        ReceiptDate = receiptDate;
    }

    public void AddLine(Guid productId, decimal qty, decimal? unitCost = null)
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
            ReceiptId: Id,
            SupplierId: SupplierId,
            WarehouseId: WarehouseId,
            PostedAtUtc: utcNow,
            Lines: _lines.Select(l => new GoodsReceiptPostedLine(l.ProductId, l.Qty, l.UnitCost)).ToList()
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
