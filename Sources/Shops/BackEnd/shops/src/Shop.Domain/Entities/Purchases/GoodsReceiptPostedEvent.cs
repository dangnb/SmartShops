using MediatR;

namespace Shop.Domain.Entities.Purchases;

public sealed record GoodsReceiptPostedEvent(
    Guid ReceiptId,
    Guid SupplierId,
    Guid WarehouseId,
    DateTime PostedAtUtc,
    IReadOnlyList<GoodsReceiptPostedLine> Lines
) : INotification;

public sealed record GoodsReceiptPostedLine(Guid ProductId, decimal Qty, decimal? UnitCost);
