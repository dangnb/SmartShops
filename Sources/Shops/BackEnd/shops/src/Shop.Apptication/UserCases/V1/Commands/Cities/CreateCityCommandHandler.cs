using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Cities;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;
using static Shop.Domain.Exceptions.CitiesException;

namespace Shop.Apptication.UserCases.V1.Commands.Cities;
public class DeleteCityCommandHandler : ICommandHandler<Command.DeleteCityCommand>
{
    private readonly IRepositoryBase<City, int> _repositoryBase;
    public DeleteCityCommandHandler(IRepositoryBase<City, int> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.DeleteCityCommand request, CancellationToken cancellationToken)
    {
        var city = await _repositoryBase.FindByIdAsync(request.Id) ?? throw new CityNotFoundException(request.Id);
        _repositoryBase.Remove(city);
        return Result.Success();
    }
}
