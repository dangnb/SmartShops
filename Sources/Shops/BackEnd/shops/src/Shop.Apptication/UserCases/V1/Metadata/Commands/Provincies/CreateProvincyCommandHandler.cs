using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Provinces;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Provincies;
public class CreateProvinceCommandHandler : ICommandHandler<Command.CreateProvinceCommand>
{
    private readonly IRepositoryBase<Province, Guid> _repositoryBase;
    public CreateProvinceCommandHandler(IRepositoryBase<Province, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.CreateProvinceCommand request, CancellationToken cancellationToken)
    {
        var entity = Province.CreateEntity( request.Code, request.Name) ;
        _repositoryBase.Add(entity);
        return Result.Success(entity);
    }
}
