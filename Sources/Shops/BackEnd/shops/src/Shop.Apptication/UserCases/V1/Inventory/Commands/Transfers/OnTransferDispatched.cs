using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Purchases;
using Shop.Domain.Inventory;

namespace Shop.Apptication.UserCases.V1.Inventory.Commands.Transfers;

public sealed class OnTransferDispatched : INotificationHandler<StockTransferDispatchedEvent>
{
    private readonly IRepositoryBase<StockMovement, Guid> _repositoryBase;
    public OnTransferDispatched(IRepositoryBase<StockMovement, Guid> repositoryBase)
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
