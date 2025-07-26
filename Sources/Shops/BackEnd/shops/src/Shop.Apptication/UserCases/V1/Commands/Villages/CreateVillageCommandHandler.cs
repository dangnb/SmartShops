using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Villages;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;

namespace Shop.Apptication.UserCases.V1.Commands.Villages;
public class CreateVillageCommandHandler : ICommandHandler<Command.CreateVillageCommand>
{
    private readonly IRepositoryBase<Village, int> _repositoryBase;
    private readonly IUserProvider _userProvider;
    public CreateVillageCommandHandler(IRepositoryBase<Village, int> repositoryBase, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _userProvider = userProvider;
    }
    public async Task<Result> Handle(Command.CreateVillageCommand request, CancellationToken cancellationToken)
    {
        var entity = Village.CreateEntity(_userProvider.GetComID(), request.WardId, request.Code, request.Name, request.username);
        _repositoryBase.Add(entity);
        return Result.Success(entity);
    }
}
