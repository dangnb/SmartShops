using MediatR;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Purchases;

namespace Shop.Apptication.UserCases.Purchasing.V1.Commands.GoodsReceipts;

public sealed class OnGoodsReceiptPosted : INotificationHandler<GoodsReceiptPostedEvent>
{
    private readonly IRepositoryBase<StockMovement, Guid> _repositoryBase;
    public OnGoodsReceiptPosted(IRepositoryBase<StockMovement, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public Task Handle(GoodsReceiptPostedEvent ev, CancellationToken ct)
    {
        foreach (GoodsReceiptPostedLine line in ev.Lines)
        {
            _repositoryBase.Add(StockMovement.Inbound(ev.PostedAtUtc, ev.WarehouseId, line.ProductId, line.Qty, line.UnitCost, "GR", ev.ReceiptId));
        }
        return Task.CompletedTask;
    }
}
