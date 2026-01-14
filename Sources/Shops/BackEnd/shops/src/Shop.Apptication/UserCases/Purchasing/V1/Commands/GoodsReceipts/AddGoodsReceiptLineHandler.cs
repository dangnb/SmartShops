using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.Purchasing.V1.GoodsReceipts;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Purchases;

namespace Shop.Apptication.UserCases.Purchasing.V1.Commands.GoodsReceipts;

internal class AddGoodsReceiptLineHandler : ICommandHandler<Command.AddGoodsReceiptLineCommand>
{
    private readonly IRepositoryBase<GoodsReceipt, Guid> _repositoryBase;
    public AddGoodsReceiptLineHandler(IRepositoryBase<GoodsReceipt, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.AddGoodsReceiptLineCommand req, CancellationToken cancellationToken)
    {
        var receipt = await _repositoryBase.FindSingleAsync(x => x.Id == req.ReceiptId, cancellationToken, x => x.Lines) ?? throw new Exception("GoodsReceipt not found.");
        receipt.AddLine(req.ProductId, req.Qty, req.UnitCost);
        _repositoryBase.Update(receipt);
        return Result.Success(receipt);
    }
}
