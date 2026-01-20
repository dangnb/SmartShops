using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.Purchasing.V1.Warehouses;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Purchases;
using static Shop.Domain.Exceptions.WarehouseException;
namespace Shop.Apptication.UserCases.V1.Purchasing.Commands.Warehouses;

public class DeleteWarehouseHandler : ICommandHandler<Command.DeleteWarehouseCommand>
{
    private readonly IRepositoryBase<Warehouse, Guid> _repositoryBase;
    private readonly ICurrentUser _currentUser;

    public DeleteWarehouseHandler(IRepositoryBase<Warehouse, Guid> repositoryBase, ICurrentUser currentUser)
    {
        _repositoryBase = repositoryBase;
        _currentUser = currentUser;
    }
    public async Task<Result> Handle(Command.DeleteWarehouseCommand request, CancellationToken cancellationToken)
    {
        Guid comId = _currentUser.GetRequiredCompanyId();
        Warehouse entity = await _repositoryBase.FindSingleAsync(x => x.ComId == comId && x.Id == request.Id, cancellationToken) ??
            throw new WarehouseNotFoundException(request.Id);
        _repositoryBase.Remove(entity);
        return Result.Success(entity);
    }
}
