using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Configs;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Metadata;

namespace Shop.Apptication.UserCases.V1.Commands.Configs;
public class CreateConfigCommandHandler : ICommandHandler<Command.CreateConfigCommand>
{
    private readonly IRepositoryBase<Config, int> _repositoryBase;
    private readonly IUserProvider _userProvider;
    public CreateConfigCommandHandler(IRepositoryBase<Config, int> repositoryBase, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _userProvider = userProvider;
    }
    public async Task<Result> Handle(Command.CreateConfigCommand request, CancellationToken cancellationToken)
    {
        var entity = Config.CreateEntity(_userProvider.GetComID(), request.Code, request.Value);
        _repositoryBase.Add(entity);
        return Result.Success(entity);
    }
}
