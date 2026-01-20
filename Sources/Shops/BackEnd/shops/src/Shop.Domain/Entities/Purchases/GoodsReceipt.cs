using Shop.Domain.Abstractions;
using Shop.Domain.Entities.enums;
using Shop.Domain.Exceptions;

namespace Shop.Domain.Entities.Purchases;

public class GoodsReceipt : EntityAuditBase<Guid>
{
    // EF Core sẽ tự dùng backing field ngầm
    protected GoodsReceipt() { }

    // 🔹 Backing field chứa các dòng hàng nhập (aggregate child)
    // 🔹 Chỉ domain mới được phép thay đổi
    private readonly List<GoodsReceiptLine> _lines = new();

    // 🔹 Số chứng từ nhập kho (unique business key, ví dụ: GR-2026-0001)
    // 🔹 Dùng để tra cứu, đối soát, in chứng từ
    public string ReceiptNo { get; private set; } = default!;

    // 🔹 Nhà cung cấp của phiếu nhập
    // 🔹 Thường map tới Supplier aggregate (không cần navigation để tránh coupling)
    public Guid SupplierId { get; private set; }

    // 🔹 Kho nhận hàng
    // 🔹 Dùng để tạo stock movement khi Post
    public Guid WarehouseId { get; private set; }

    // 🔹 Ngày nhập hàng (ngày nghiệp vụ, KHÔNG phải CreatedAt)
    // 🔹 DateOnly để tránh lệch múi giờ
    public DateOnly ReceiptDate { get; private set; }

    // 🔹 Trạng thái chứng từ:
    // 🔹 Draft     : đang tạo, cho phép sửa
    // 🔹 Posted    : đã ghi sổ, phát sinh tồn kho
    // 🔹 Cancelled : đã huỷ (không được chỉnh sửa)
    public DocumentStatus Status { get; private set; } = DocumentStatus.Draft;

    // 🔹 Tổng tiền trước thuế / phí
    // 🔹 Được tính từ tổng (Qty × UnitCost) của các dòng
    public decimal Subtotal { get; private set; }

    // 🔹 Tổng tiền cuối cùng của phiếu nhập
    // 🔹 Có thể = Subtotal + Tax + Surcharge - Discount
    public decimal Total { get; private set; }

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

        _lines.Add(
            new GoodsReceiptLine(
                Id,
                productId,
                qty,
                unitCost));
        Recalc();
    }

    public void Post(DateTime utcNow)
    {
        EnsureDraft();

        if (_lines.Count == 0)
            throw new DomainBaseException("Cannot post receipt without lines.");

        Status = DocumentStatus.Posted;

        RaiseDomainEvent(new GoodsReceiptPostedEvent(
            Id,
            SupplierId,
            WarehouseId,
            utcNow,
            [.. _lines.Select(l =>
                new GoodsReceiptPostedLine(l.ProductId, l.Qty, l.UnitCost)
            )]
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

    public void RemoveLine(Guid lineId)
    {
        EnsureDraft();

        var line = _lines.FirstOrDefault(x => x.Id == lineId)
            ?? throw new DomainBaseException("Line not found.");

        _lines.Remove(line);
        Recalc();
    }

    public void UpdateLine(Guid lineId, decimal qty, decimal? unitCost)
    {
        EnsureDraft();

        if (qty <= 0)
            throw new DomainBaseException("Qty must be > 0.");

        var line = _lines.FirstOrDefault(x => x.Id == lineId)
            ?? throw new DomainBaseException("Line not found.");

        line.Update(qty, unitCost);
        Recalc();
    }
}

