using MediatR;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Inventory;
using Shop.Domain.Entities.Purchases;

namespace Shop.Apptication.UserCases.V1.Inventory.Events.Transfers;

public sealed class OnTransferDispatchedEvent : INotificationHandler<StockTransferDispatchedEvent>
{
    private readonly IRepositoryBase<StockMovement, Guid> _repositoryBase;
    public OnTransferDispatchedEvent(IRepositoryBase<StockMovement, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public Task Handle(StockTransferDispatchedEvent ev, CancellationToken ct)
    {
        foreach (var l in ev.Lines)
        {
            _repositoryBase.Add(StockMovement.Outbound(ev.AtUtc, ev.FromWarehouseId, l.ProductId, l.Qty, null, "TR_OUT", ev.TransferId));
        }
        return Task.CompletedTask;
    }
}
