using MediatR;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Inventory;
using Shop.Domain.Entities.Purchases;

namespace Shop.Apptication.UserCases.V1.Inventory.Events.Adjustments;

public sealed class OnAdjustmentPostedEvent : INotificationHandler<InventoryAdjustmentPostedEvent>
{
    private readonly IRepositoryBase<StockMovement, Guid> _repositoryBase;
    public OnAdjustmentPostedEvent(IRepositoryBase<StockMovement, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public Task Handle(InventoryAdjustmentPostedEvent ev, CancellationToken ct)
    {
        foreach (var l in ev.Lines)
        {
            if (l.QtyDelta > 0)
                _repositoryBase.Add(StockMovement.Inbound(ev.AtUtc, ev.WarehouseId, l.ProductId, l.QtyDelta, l.UnitCost, "ADJ_IN", ev.AdjustmentId));
            else
                _repositoryBase.Add(StockMovement.Outbound(ev.AtUtc, ev.WarehouseId, l.ProductId, Math.Abs(l.QtyDelta), l.UnitCost, "ADJ_OUT", ev.AdjustmentId));
        }
        return Task.CompletedTask;
    }
}
