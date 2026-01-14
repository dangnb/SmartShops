using MediatR;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Purchases;
using Shop.Domain.Inventory;

namespace Shop.Apptication.UserCases.Inventory.V1.Commands.Transfers;

public sealed class OnTransferReceived : INotificationHandler<StockTransferReceivedEvent>
{
    private readonly IRepositoryBase<StockMovement, Guid> _repositoryBase;
    public OnTransferReceived(IRepositoryBase<StockMovement, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public Task Handle(StockTransferReceivedEvent ev, CancellationToken ct)
    {
        foreach (var l in ev.Lines)
        {
            _repositoryBase.Add(StockMovement.Inbound(ev.AtUtc, ev.ToWarehouseId, l.ProductId, l.Qty, null, "TR_IN", ev.TransferId));
        }
        return Task.CompletedTask;
    }
}
