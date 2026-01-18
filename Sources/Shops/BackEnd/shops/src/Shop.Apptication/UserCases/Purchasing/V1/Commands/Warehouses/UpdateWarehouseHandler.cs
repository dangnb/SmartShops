using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.Purchasing.V1.Warehouses;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Purchases;
using static Shop.Domain.Exceptions.WarehouseException;
namespace Shop.Apptication.UserCases.Purchasing.V1.Commands.Warehouses;

public class UpdateWarehouseHandler : ICommandHandler<Command.UpdateWarehouseCommand>
{
    private readonly IRepositoryBase<Warehouse, Guid> _repositoryBase;
    private readonly ICurrentUser _currentUser;

    public UpdateWarehouseHandler(IRepositoryBase<Warehouse, Guid> repositoryBase, ICurrentUser currentUser)
    {
        _repositoryBase = repositoryBase;
        _currentUser = currentUser;
    }
    public async Task<Result> Handle(Command.UpdateWarehouseCommand request, CancellationToken cancellationToken)
    {
        Guid comId = _currentUser.GetRequiredCompanyId();
        Warehouse entity = await _repositoryBase.FindSingleAsync(x => x.ComId == comId && x.Id == request.Id, cancellationToken) ??
            throw new WarehouseNotFoundException(request.Id);
        entity.Update(request.Name, request.Address, request.IsActive);
        _repositoryBase.Update(entity);
        return Result.Success(entity);
    }
}
