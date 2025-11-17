using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Wards;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.WardsException;

namespace TP.Apptication.UserCases.V1.Commands.Wards;

public class UpdateWardCommandHandler : ICommandHandler<Command.UpdateWardCommand>
{
    private readonly IRepositoryBase<Ward, Guid> _repositoryBase;
    public UpdateWardCommandHandler(IRepositoryBase<Ward, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.UpdateWardCommand request, CancellationToken cancellationToken)
    {

        var city = await _repositoryBase.FindByIdAsync(request.Id)
            ?? throw new WardNotFoundException(request.Id);
        city.Update(request.Code, request.Name);
        _repositoryBase.Update(city);
        return Result.Success(city);
    }
}
