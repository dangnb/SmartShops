using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Configs;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Commands.Configs;
public class CreateConfigCommandHandler : ICommandHandler<Command.CreateConfigCommand>
{
    private readonly IRepositoryBase<Config, int> _repositoryBase;
    private readonly ICurrentUser _currentUser;
    public CreateConfigCommandHandler(IRepositoryBase<Config, int> repositoryBase, ICurrentUser currentUser)
    {
        _repositoryBase = repositoryBase;
        _currentUser = currentUser;
    }
    public async Task<Result> Handle(Command.CreateConfigCommand request, CancellationToken cancellationToken)
    {
        var entity = Config.CreateEntity(_currentUser.GetRequiredCompanyId(), request.Code, request.Value);
        _repositoryBase.Add(entity);
        return Result.Success(entity);
    }
}
