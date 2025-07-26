using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Wards;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;
using static Shop.Domain.Exceptions.VillagesException;
using static Shop.Domain.Exceptions.WardsException;

namespace Shop.Apptication.UserCases.V1.Commands.Wards;
public class UpdateWardCommandHandler : ICommandHandler<Command.UpdateWardCommand>
{
    private readonly IRepositoryBase<Ward, int> _repositoryBase;
    private readonly IUserProvider _userProvider;
    public UpdateWardCommandHandler(IRepositoryBase<Ward, int> repositoryBase, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _userProvider = userProvider;
    }
    public async Task<Result> Handle(Command.UpdateWardCommand request, CancellationToken cancellationToken)
    {

        var city = await _repositoryBase.FindByIdAsync(request.Id) 
            ?? throw new WardNotFoundException(request.Id);
        city.Update(request.DistrictId, request.Code, request.Name);
        _repositoryBase.Update(city);
        return Result.Success(city);
    }
}
