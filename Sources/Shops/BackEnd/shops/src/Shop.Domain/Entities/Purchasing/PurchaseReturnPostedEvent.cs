using MediatR;

namespace Shop.Domain.Entities.Purchasing;

public sealed record PurchaseReturnPostedEvent(
    Guid ReturnId, Guid SupplierId, Guid WarehouseId, DateTime AtUtc,
    IReadOnlyList<PurchaseReturnLinePayload> Lines
) : INotification;

public sealed record PurchaseReturnLinePayload(Guid ProductId, decimal Qty);
