using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Abstractions;

namespace Shop.Domain.Entities.Purchases;

public class StockMovement : DomainEntity<Guid>
{
    public DateTime MoveAtUtc { get; private set; }
    public Guid WarehouseId { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal QtyIn { get; private set; }
    public decimal QtyOut { get; private set; }
    public decimal? UnitCost { get; private set; }

    public string RefType { get; private set; } = default!;
    public Guid RefId { get; private set; }

    protected StockMovement() { }

    public static StockMovement Inbound(DateTime atUtc, Guid whId, Guid productId, decimal qty, decimal? unitCost, string refType, Guid refId)
        => new StockMovement { MoveAtUtc = atUtc, WarehouseId = whId, ProductId = productId, QtyIn = qty, QtyOut = 0, UnitCost = unitCost, RefType = refType, RefId = refId };

    public static StockMovement Outbound(DateTime atUtc, Guid whId, Guid productId, decimal qty, decimal? unitCost, string refType, Guid refId)
        => new StockMovement { MoveAtUtc = atUtc, WarehouseId = whId, ProductId = productId, QtyIn = 0, QtyOut = qty, UnitCost = unitCost, RefType = refType, RefId = refId };
}
