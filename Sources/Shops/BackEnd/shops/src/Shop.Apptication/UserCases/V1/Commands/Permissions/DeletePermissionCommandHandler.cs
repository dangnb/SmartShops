using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Permissions;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Identity;
using Shop.Domain.Exceptions;
using static Shop.Domain.Exceptions.PermissionsException;

namespace Shop.Apptication.UserCases.V1.Commands.Permissions;
public class DeletePermissionCommandHandler : ICommandHandler<Command.DeletePermissionCommand>
{
    private readonly IRepositoryBase<Permission, Guid> _repositoryBase;
    public DeletePermissionCommandHandler(IRepositoryBase<Permission, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        var district = await _repositoryBase.FindByIdAsync(request.ID) 
            ?? throw new PermissionNotFoundException(request.ID);
        _repositoryBase.Remove(district);
        return Result.Success();
    }
}
