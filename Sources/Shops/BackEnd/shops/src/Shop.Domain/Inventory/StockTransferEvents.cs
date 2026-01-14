using MediatR;

namespace Shop.Domain.Inventory;

public sealed record StockTransferDispatchedEvent(
    Guid TransferId, Guid FromWarehouseId, Guid ToWarehouseId, DateTime AtUtc,
    IReadOnlyList<TransferLinePayload> Lines
) : INotification;

public sealed record StockTransferReceivedEvent(
    Guid TransferId, Guid FromWarehouseId, Guid ToWarehouseId, DateTime AtUtc,
    IReadOnlyList<TransferLinePayload> Lines
) : INotification;

public sealed record TransferLinePayload(Guid ProductId, decimal Qty);
