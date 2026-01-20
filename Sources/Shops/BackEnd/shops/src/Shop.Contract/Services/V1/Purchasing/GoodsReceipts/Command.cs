using MediatR;
using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Purchasing.GoodsReceipts;

public static class Command
{
    public sealed record CreateGoodsReceiptCommand(string ReceiptNo, Guid SupplierId, Guid WarehouseId, DateOnly ReceiptDate) : ICommand;
    public sealed record AddGoodsReceiptLineCommand(Guid ReceiptId, Guid ProductId, decimal Qty, decimal? UnitCost) : ICommand;
    public sealed record PostGoodsReceiptCommand(Guid ReceiptId) : ICommand;

}
