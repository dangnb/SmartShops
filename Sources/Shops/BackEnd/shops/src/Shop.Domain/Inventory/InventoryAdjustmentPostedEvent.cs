using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Shop.Domain.Inventory;

public sealed record InventoryAdjustmentPostedEvent(
    Guid AdjustmentId, Guid WarehouseId, DateTime AtUtc,
    IReadOnlyList<AdjustmentLinePayload> Lines
) : INotification;

public sealed record AdjustmentLinePayload(Guid ProductId, decimal QtyDelta, decimal? UnitCost);
