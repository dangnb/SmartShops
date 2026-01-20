using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.Purchasing.V1.GoodsReceipts;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Purchases;

namespace Shop.Apptication.UserCases.V1.Purchasing.Commands.GoodsReceipts;


public sealed class CreateGoodsReceiptHandler : ICommandHandler<Command.CreateGoodsReceiptCommand>
{
    private readonly IRepositoryBase<GoodsReceipt, Guid> _repositoryBase;
    public CreateGoodsReceiptHandler(IRepositoryBase<GoodsReceipt, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.CreateGoodsReceiptCommand req, CancellationToken cancellationToken)
    {
        var receipt = new GoodsReceipt(req.ReceiptNo, req.SupplierId, req.WarehouseId, req.ReceiptDate);
        _repositoryBase.Add(receipt);
        return Result.Success(receipt);
    }
}
