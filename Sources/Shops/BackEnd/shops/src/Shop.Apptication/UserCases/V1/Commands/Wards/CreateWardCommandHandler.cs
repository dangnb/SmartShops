using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Wards;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Commands.Wards;
public class CreateWardCommandHandler : ICommandHandler<Command.CreateWardCommand>
{
    private readonly IRepositoryBase<Ward, Guid> _repositoryBase;
    public CreateWardCommandHandler(IRepositoryBase<Ward, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.CreateWardCommand request, CancellationToken cancellationToken)
    {
        var entity = Ward.CreateEntity(  request.Code, request.Name, request.ProvincyId);
        _repositoryBase.Add(entity);
        return Result.Success(entity);
    }
}
