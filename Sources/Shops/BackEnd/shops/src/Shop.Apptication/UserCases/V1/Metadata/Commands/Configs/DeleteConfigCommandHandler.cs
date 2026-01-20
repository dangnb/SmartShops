using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Configs;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Configs;
public class DeleteConfigCommandHandler : ICommandHandler<Command.DeleteConfigCommand>
{
    private readonly IRepositoryBase<Config, int> _repositoryBase;
    public DeleteConfigCommandHandler(IRepositoryBase<Config, int> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.DeleteConfigCommand request, CancellationToken cancellationToken)
    {
        var config = await _repositoryBase.FindByIdAsync(request.Id)
            ?? throw new DistrictsException.DistrictNotFoundException(request.Id);
        _repositoryBase.Remove(config);
        return Result.Success();
    }
}
