using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Common.Configs;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.DistrictsException;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Configs;
public class UpdateCityCommandHandler : ICommandHandler<Command.UpdateConfigCommand>
{
    private readonly IRepositoryBase<Config, int> _repositoryBase;
    private readonly ICurrentUser _userProvider;
    public UpdateCityCommandHandler(IRepositoryBase<Config, int> repositoryBase, ICurrentUser userProvider)
    {
        _repositoryBase = repositoryBase;
        _userProvider = userProvider;
    }
    public async Task<Result> Handle(Command.UpdateConfigCommand request, CancellationToken cancellationToken)
    {

        var city = await _repositoryBase.FindByIdAsync(request.Id)
            ?? throw new DistrictNotFoundException(request.Id);
        city.Update(request.Code, request.Value);
        _repositoryBase.Update(city);
        return Result.Success(city);
    }
}
