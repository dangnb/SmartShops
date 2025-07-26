using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Districts;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;

namespace Shop.Apptication.UserCases.V1.Commands.Districts;
public class CreateDistrictCommandHandler : ICommandHandler<Command.CreateDistrictCommand>
{
    private readonly IRepositoryBase<District, int> _repositoryBase;
    private readonly IUserProvider _userProvider;
    public CreateDistrictCommandHandler(IRepositoryBase<District, int> repositoryBase, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _userProvider = userProvider;
    }
    public async Task<Result> Handle(Command.CreateDistrictCommand request, CancellationToken cancellationToken)
    {
        var entity = District.CreateEntity(_userProvider.GetComID(), request.CityId, request.Code, request.Name);
        _repositoryBase.Add(entity);
        return Result.Success(entity);
    }
}
