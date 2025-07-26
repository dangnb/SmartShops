using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Villages;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;
using static Shop.Domain.Exceptions.VillagesException;

namespace Shop.Apptication.UserCases.V1.Commands.Villages;
public class UpdateVillageCommandHandler : ICommandHandler<Command.UpdateVillageCommand>
{
    private readonly IRepositoryBase<Village, int> _repositoryBase;
    private readonly IUserProvider _userProvider;
    public UpdateVillageCommandHandler(IRepositoryBase<Village, int> repositoryBase, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _userProvider = userProvider;
    }
    public async Task<Result> Handle(Command.UpdateVillageCommand request, CancellationToken cancellationToken)
    {

        var city = await _repositoryBase.FindByIdAsync(request.Id) ?? throw new VillageNotFoundException(request.Id);
        city.Update(request.WardId, request.Code, request.Name, request.username);
        _repositoryBase.Update(city);
        return Result.Success(city);
    }
}
