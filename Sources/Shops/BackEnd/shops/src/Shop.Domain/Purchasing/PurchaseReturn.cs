using Shop.Domain.Abstractions;
using Shop.Domain.Entities.enums;
using Shop.Domain.Exceptions;

namespace Shop.Domain.Purchasing;

public sealed class PurchaseReturn : EntityAuditBase<Guid>
{
    private readonly List<PurchaseReturnLine> _lines = new();

    public string ReturnNo { get; private set; } = default!;
    public Guid SupplierId { get; private set; }
    public Guid WarehouseId { get; private set; }
    public DateOnly ReturnDate { get; private set; }
    public DocumentStatus Status { get; private set; } = DocumentStatus.Draft;

    public IReadOnlyCollection<PurchaseReturnLine> Lines => _lines.AsReadOnly();

    private PurchaseReturn() { }

    public PurchaseReturn(string returnNo, Guid supplierId, Guid warehouseId, DateOnly date)
    {
        if (string.IsNullOrWhiteSpace(returnNo))
            throw new DomainBaseException("ReturnNo is required.");
        ReturnNo = returnNo.Trim();
        SupplierId = supplierId;
        WarehouseId = warehouseId;
        ReturnDate = date;
    }

    public void AddLine(Guid productId, decimal qty)
    {
        EnsureDraft();
        if (qty <= 0)
            throw new DomainBaseException("Qty must be > 0.");
        _lines.Add(new PurchaseReturnLine(productId, qty));
    }

    public void Post(DateTime utcNow)
    {
        EnsureDraft();
        if (_lines.Count == 0)
            throw new DomainBaseException("Return must have lines.");
        Status = DocumentStatus.Posted;

        Raise(new PurchaseReturnPostedEvent(Id, SupplierId, WarehouseId, utcNow,
            _lines.Select(l => new PurchaseReturnLinePayload(l.ProductId, l.Qty)).ToList()));
    }

    private void EnsureDraft()
    {
        if (Status != DocumentStatus.Draft)
            throw new DomainBaseException("Only Draft return can be modified.");
    }
}

public sealed class PurchaseReturnLine
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid PurchaseReturnId { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal Qty { get; private set; }

    private PurchaseReturnLine() { }
    internal PurchaseReturnLine(Guid productId, decimal qty)
    {
        ProductId = productId;
        Qty = qty;
    }
}
