using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Wards;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;

namespace Shop.Apptication.UserCases.V1.Commands.Wards;
public class CreateWardCommandHandler : ICommandHandler<Command.CreateWardCommand>
{
    private readonly IRepositoryBase<Ward, int> _repositoryBase;
    private readonly IUserProvider _userProvider;
    public CreateWardCommandHandler(IRepositoryBase<Ward, int> repositoryBase, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _userProvider = userProvider;
    }
    public async Task<Result> Handle(Command.CreateWardCommand request, CancellationToken cancellationToken)
    {
        var entity = Ward.CreateEntity(_userProvider.GetComID(), request.DistrictId, request.Code, request.Name);
        _repositoryBase.Add(entity);
        return Result.Success(entity);
    }
}
