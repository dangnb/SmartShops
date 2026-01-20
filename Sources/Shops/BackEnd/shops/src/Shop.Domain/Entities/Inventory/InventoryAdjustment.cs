using Shop.Domain.Abstractions;
using Shop.Domain.Entities.enums;
using Shop.Domain.Exceptions;

namespace Shop.Domain.Entities.Inventory;

public class InventoryAdjustment : EntityAuditBase<Guid>
{
    private readonly List<InventoryAdjustmentLine> _lines = new();

    public string AdjustmentNo { get; private set; } = default!;
    public Guid WarehouseId { get; private set; }
    public DateOnly AdjustmentDate { get; private set; }
    public DocumentStatus Status { get; private set; } = DocumentStatus.Draft;

    // ✅ navigation property phải virtual
    public virtual IReadOnlyCollection<InventoryAdjustmentLine> Lines
        => _lines.AsReadOnly();

    protected InventoryAdjustment() { } // ✅ cho EF Proxy

    public InventoryAdjustment(string no, Guid warehouseId, DateOnly date)
    {
        if (string.IsNullOrWhiteSpace(no))
            throw new DomainBaseException("AdjustmentNo is required.");

        AdjustmentNo = no.Trim();
        WarehouseId = warehouseId;
        AdjustmentDate = date;
    }

    public void AddLine(Guid productId, decimal qtyDelta, decimal? unitCost = null)
    {
        EnsureDraft();

        if (qtyDelta == 0)
            throw new DomainBaseException("QtyDelta cannot be 0.");

        _lines.Add(new InventoryAdjustmentLine(productId, qtyDelta, unitCost));
    }

    public void Post(DateTime utcNow)
    {
        EnsureDraft();

        if (_lines.Count == 0)
            throw new DomainBaseException("Adjustment must have lines.");

        Status = DocumentStatus.Posted;

        RaiseDomainEvent(new InventoryAdjustmentPostedEvent(
            Id,
            WarehouseId,
            utcNow,
            _lines.Select(l =>
                new AdjustmentLinePayload(
                    l.ProductId,
                    l.QtyDelta,
                    l.UnitCost))
            .ToList()
        ));
    }

    private void EnsureDraft()
    {
        if (Status != DocumentStatus.Draft)
            throw new DomainBaseException("Only Draft adjustment can be modified.");
    }
}
