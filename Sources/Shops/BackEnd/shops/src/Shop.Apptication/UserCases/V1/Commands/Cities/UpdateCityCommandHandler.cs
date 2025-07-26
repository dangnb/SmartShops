using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Cities;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;
using static Shop.Domain.Exceptions.CitiesException;

namespace Shop.Apptication.UserCases.V1.Commands.Cities;
public class UpdateCityCommandHandler : ICommandHandler<Command.UpdateCityCommand>
{
    private readonly IRepositoryBase<City, int> _repositoryBase;
    private readonly IUserProvider _userProvider;
    public UpdateCityCommandHandler(IRepositoryBase<City, int> repositoryBase, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _userProvider = userProvider;
    }
    public async Task<Result> Handle(Command.UpdateCityCommand request, CancellationToken cancellationToken)
    {

        var city = await _repositoryBase.FindByIdAsync(request.Id) ?? throw new CityNotFoundException(request.Id);
        city.Update(request.Code, request.Name);
        _repositoryBase.Update(city);
        return Result.Success(city);
    }
}
