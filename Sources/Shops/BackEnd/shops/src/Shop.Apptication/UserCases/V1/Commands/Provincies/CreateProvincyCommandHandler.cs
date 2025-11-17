using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Provincies;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace TP.Apptication.UserCases.V1.Commands.Provincies;
public class CreateProvincyCommandHandler : ICommandHandler<Command.CreateProvincyCommand>
{
    private readonly IRepositoryBase<Provincy, Guid> _repositoryBase;
    public CreateProvincyCommandHandler(IRepositoryBase<Provincy, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.CreateProvincyCommand request, CancellationToken cancellationToken)
    {
        var entity = Provincy.CreateEntity( request.Code, request.Name) ;
        _repositoryBase.Add(entity);
        return Result.Success(entity);
    }
}
