using MediatR;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Purchases;
using Shop.Domain.Purchasing;

namespace Shop.Apptication.UserCases.V1.Purchasing.Commands.Returns;

public sealed class OnPurchaseReturnPosted : INotificationHandler<PurchaseReturnPostedEvent>
{
    private readonly IRepositoryBase<StockMovement, Guid> _repositoryBase;
    public OnPurchaseReturnPosted(IRepositoryBase<StockMovement, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }


    public Task Handle(PurchaseReturnPostedEvent ev, CancellationToken ct)
    {
        foreach (var l in ev.Lines)
        {
            _repositoryBase.Add(StockMovement.Outbound(ev.AtUtc, ev.WarehouseId, l.ProductId, l.Qty, null, "PR", ev.ReturnId));
        }
        return Task.CompletedTask;
    }
}
