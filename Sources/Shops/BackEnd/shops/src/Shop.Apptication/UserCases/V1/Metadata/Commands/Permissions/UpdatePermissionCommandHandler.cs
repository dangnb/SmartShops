using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Permissions;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Identity;
using static Shop.Domain.Exceptions.PermissionsException;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Permissions;
public class UpdatePermissionCommandHandler : ICommandHandler<Command.UpdatePermissionCommand>
{
    private readonly IRepositoryBase<Permission, Guid> _repositoryBase;
    private readonly ICurrentUser _userProvider;
    public UpdatePermissionCommandHandler(IRepositoryBase<Permission, Guid> repositoryBase, ICurrentUser userProvider)
    {
        _repositoryBase = repositoryBase;
        _userProvider = userProvider;
    }
    public async Task<Result> Handle(Command.UpdatePermissionCommand request, CancellationToken cancellationToken)
    {

        var city = await _repositoryBase.FindByIdAsync(request.ID)
            ?? throw new PermissionNotFoundException(request.ID);
        city.Update(request.Code, request.Description, request.GroupCode, request.GroupName);
        _repositoryBase.Update(city);
        return Result.Success(city);
    }
}
