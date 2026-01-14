using Shop.Domain.Abstractions;
using Shop.Domain.Entities.enums;
using Shop.Domain.Exceptions;

namespace Shop.Domain.Inventory;

public class StockTransfer:EntityAuditBase<Guid>
{
    private readonly List<StockTransferLine> _lines = new();

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string TransferNo { get; private set; } = default!;
    public Guid FromWarehouseId { get; private set; }
    public Guid ToWarehouseId { get; private set; }
    public DateOnly TransferDate { get; private set; }
    public DocumentStatus Status { get; private set; } = DocumentStatus.Draft;

    public IReadOnlyCollection<StockTransferLine> Lines => _lines.AsReadOnly();

    private StockTransfer() { }

    public StockTransfer(string transferNo, Guid fromWh, Guid toWh, DateOnly date)
    {
        if (string.IsNullOrWhiteSpace(transferNo))
            throw new DomainBaseException("TransferNo is required.");
        if (fromWh == toWh)
            throw new DomainBaseException("FromWarehouse and ToWarehouse must differ.");
        TransferNo = transferNo.Trim();
        FromWarehouseId = fromWh;
        ToWarehouseId = toWh;
        TransferDate = date;
    }

    public void AddLine(Guid productId, decimal qty)
    {
        EnsureDraft();
        if (qty <= 0)
            throw new DomainBaseException("Qty must be > 0.");
        _lines.Add(new StockTransferLine(productId, qty));
    }

    public void Dispatch(DateTime utcNow)
    {
        EnsureDraft();
        if (_lines.Count == 0)
            throw new DomainBaseException("Transfer must have lines.");
        Status = DocumentStatus.Dispatched;

        Raise(new StockTransferDispatchedEvent(Id, FromWarehouseId, ToWarehouseId, utcNow,
            _lines.Select(l => new TransferLinePayload(l.ProductId, l.Qty)).ToList()));
    }

    public void Receive(DateTime utcNow)
    {
        if (Status != DocumentStatus.Dispatched)
            throw new DomainBaseException("Transfer must be Dispatched before Receive.");
        Status = DocumentStatus.Received;

        Raise(new StockTransferReceivedEvent(Id, FromWarehouseId, ToWarehouseId, utcNow,
            _lines.Select(l => new TransferLinePayload(l.ProductId, l.Qty)).ToList()));
    }

    private void EnsureDraft()
    {
        if (Status != DocumentStatus.Draft)
            throw new DomainBaseException("Only Draft transfer can be modified.");
    }
}

public sealed class StockTransferLine
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid StockTransferId { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal Qty { get; private set; }

    private StockTransferLine() { }
    internal StockTransferLine(Guid productId, decimal qty)
    {
        ProductId = productId;
        Qty = qty;
    }
}
