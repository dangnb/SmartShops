using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Cities;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;

namespace Shop.Apptication.UserCases.V1.Commands.Cities;
public class CreateCityCommandHandler : ICommandHandler<Command.CreateCityCommand>
{
    private readonly IRepositoryBase<City, int> _repositoryBase;
    private readonly IUserProvider _userProvider;
    public CreateCityCommandHandler(IRepositoryBase<City, int> repositoryBase, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _userProvider = userProvider;
    }
    public async Task<Result> Handle(Command.CreateCityCommand request, CancellationToken cancellationToken)
    {
        var entity = City.CreateEntity(_userProvider.GetComID(), request.Code, request.Name) ;
        _repositoryBase.Add(entity);
        return Result.Success(entity);
    }
}
