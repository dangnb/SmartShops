using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Inventory;

namespace Shop.Apptication.UserCases.Inventory.V1.Commands.Transfers;

public sealed record DispatchTransferCommand(Guid TransferId) : ICommand;

public sealed class DispatchTransferHandler : ICommandHandler<DispatchTransferCommand>
{
    private readonly IRepositoryBase<StockTransfer, Guid> _repositoryBase;
    public DispatchTransferHandler(IRepositoryBase<StockTransfer, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public async Task<Result> Handle(DispatchTransferCommand request, CancellationToken cancellationToken)
    {
        var transfer = await _repositoryBase.FindSingleAsync(t => t.Id == request.TransferId, cancellationToken, t => t.Lines)
           ?? throw new Exception("Transfer not found.");

        transfer.Dispatch(DateTime.UtcNow);
        _repositoryBase.Update(transfer);
        return Result.Success(transfer);
    }
}

