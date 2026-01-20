using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.Purchasing.V1.GoodsReceipts;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Purchases;

namespace Shop.Apptication.UserCases.V1.Purchasing.Commands.GoodsReceipts;

internal class PostGoodsReceiptHandler : ICommandHandler<Command.PostGoodsReceiptCommand>
{
    private readonly IRepositoryBase<GoodsReceipt, Guid> _repositoryBase;
    public PostGoodsReceiptHandler(IRepositoryBase<GoodsReceipt, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public async Task<Result> Handle(Command.PostGoodsReceiptCommand req, CancellationToken cancellationToken)
    {

        GoodsReceipt receipt = await _repositoryBase.FindSingleAsync(x => x.Id == req.ReceiptId, cancellationToken, x => x.Lines) ?? throw new Exception("GoodsReceipt not found.");
        receipt.Post(DateTime.UtcNow);
        _repositoryBase.Update(receipt);
        return Result.Success(receipt);
    }
}
