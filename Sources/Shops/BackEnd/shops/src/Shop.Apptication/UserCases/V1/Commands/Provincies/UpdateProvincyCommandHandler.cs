using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Provincies;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.CitiesException;

namespace TP.Apptication.UserCases.V1.Commands.Provincies;
public class UpdateProvincyCommandHandler : ICommandHandler<Command.UpdateProvincyCommand>
{
    private readonly IRepositoryBase<Provincy, int> _repositoryBase;
    public UpdateProvincyCommandHandler(IRepositoryBase<Provincy, int> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.UpdateProvincyCommand request, CancellationToken cancellationToken)
    {

        var city = await _repositoryBase.FindByIdAsync(request.Id) ?? throw new CityNotFoundException(request.Id);
        city.Update(request.Code, request.Name);
        _repositoryBase.Update(city);
        return Result.Success(city);
    }
}
