using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Permissions;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Identity;
namespace Shop.Apptication.UserCases.V1.Permissions.Permissions;
public class CreatePermissionCommandHandler : ICommandHandler<Command.CreatePermissionCommand>
{
    private readonly IRepositoryBase<Permission, Guid> _repositoryBase;
    private readonly IUserProvider _userProvider;
    public CreatePermissionCommandHandler(IRepositoryBase<Permission, Guid> repositoryBase, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _userProvider = userProvider;
    }
    public async Task<Result> Handle(Command.CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = Permission.CreateEntity( request.Code, request.Description, request.GroupCode, request.GroupName);
        _repositoryBase.Add(entity);
        return Result.Success(entity);
    }
}
