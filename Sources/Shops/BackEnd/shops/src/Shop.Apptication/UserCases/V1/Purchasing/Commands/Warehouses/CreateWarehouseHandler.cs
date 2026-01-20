using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.Purchasing.V1.Warehouses;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Purchases;
using static Shop.Domain.Exceptions.WarehouseException;
namespace Shop.Apptication.UserCases.V1.Purchasing.Commands.Warehouses;

public class CreateWarehouseHandler : ICommandHandler<Command.CreateWarehouseCommand>
{
    private readonly IRepositoryBase<Warehouse, Guid> _repositoryBase;
    private readonly ICurrentUser _currentUser;

    public CreateWarehouseHandler(IRepositoryBase<Warehouse, Guid> repositoryBase, ICurrentUser currentUser)
    {
        _repositoryBase = repositoryBase;
        _currentUser = currentUser;
    }
    public async Task<Result> Handle(Command.CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        Guid comId = _currentUser.GetRequiredCompanyId();
        var entity = Warehouse.CreateEntity(request.Name, request.Code, request.Address, request.IsActive);
        if (await _repositoryBase.AnyAsync(w => w.Code == entity.Code && w.ComId == comId, cancellationToken))
            throw new DuplicateCodeException(request.Code);
        _repositoryBase.Add(entity);
        return Result.Success(entity);
    }
}
