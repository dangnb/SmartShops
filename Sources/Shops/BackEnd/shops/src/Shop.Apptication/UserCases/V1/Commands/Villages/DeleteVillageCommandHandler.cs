using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Villages;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;
using static Shop.Domain.Exceptions.DistrictsException;
using static Shop.Domain.Exceptions.VillagesException;

namespace Shop.Apptication.UserCases.V1.Commands.Villages;
public class DeleteVillageCommandHandler : ICommandHandler<Command.DeleteVillageCommand>
{
    private readonly IRepositoryBase<Village, int> _repositoryBase;
    public DeleteVillageCommandHandler(IRepositoryBase<Village, int> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.DeleteVillageCommand request, CancellationToken cancellationToken)
    {
        var district = await _repositoryBase.FindByIdAsync(request.Id) ?? throw new VillageNotFoundException(request.Id);
        _repositoryBase.Remove(district);
        return Result.Success();
    }
}
