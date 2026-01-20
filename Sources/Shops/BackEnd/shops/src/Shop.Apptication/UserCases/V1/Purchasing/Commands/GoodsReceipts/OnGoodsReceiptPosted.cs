using MediatR;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Purchases;

namespace Shop.Apptication.UserCases.V1.Purchasing.Commands.GoodsReceipts;

public sealed class OnGoodsReceiptPosted(IRepositoryBase<StockMovement, Guid> repositoryBase) : INotificationHandler<GoodsReceiptPostedEvent>
{
    private readonly IRepositoryBase<StockMovement, Guid> _repositoryBase = repositoryBase;
    public async Task Handle(GoodsReceiptPostedEvent ev, CancellationToken ct)
    {
        throw new Exception("Có lỗi trong quá trình xử lý");
        foreach (GoodsReceiptPostedLine line in ev.Lines)
        {
            _repositoryBase.Add(StockMovement.Inbound(ev.PostedAtUtc, ev.WarehouseId, line.ProductId, line.Qty, line.UnitCost, "GR", ev.ReceiptId));
        }
    }
}
