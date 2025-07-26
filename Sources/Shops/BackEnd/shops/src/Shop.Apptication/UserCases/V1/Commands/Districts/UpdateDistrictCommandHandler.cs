using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Districts;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;
using Shop.Domain.Exceptions;
using static Shop.Domain.Exceptions.CitiesException;
using static Shop.Domain.Exceptions.DistrictsException;

namespace Shop.Apptication.UserCases.V1.Commands.Districts;
public class UpdateDistrictCommandHandler : ICommandHandler<Command.UpdateDistrictCommand>
{
    private readonly IRepositoryBase<District, int> _repositoryBase;
    private readonly IRepositoryBase<City, int> _cityRepository;
    private readonly IUserProvider _userProvider;
    public UpdateDistrictCommandHandler(IRepositoryBase<District, int> repositoryBase, IRepositoryBase<City, int> cityRepository, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _userProvider = userProvider;
        _cityRepository = cityRepository;
    }
    public async Task<Result> Handle(Command.UpdateDistrictCommand request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.FindByIdAsync(request.CityId)
            ?? throw new CityNotFoundException(request.CityId);
        var district = await _repositoryBase.FindByIdAsync(request.Id) 
            ?? throw new DistrictNotFoundException(request.Id);
        district.Update(request.CityId, request.Code, request.Name, city);
        _repositoryBase.Update(district);
        return Result.Success(district);
    }
}
