using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Common.Provincies;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.CitiesException;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Provincies;

public class DeleteProvinceCommandHandler : ICommandHandler<Command.DeleteProvinceCommand>
{
    private readonly IRepositoryBase<Province, Guid> _repositoryBase;
    public DeleteProvinceCommandHandler(IRepositoryBase<Province, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.DeleteProvinceCommand request, CancellationToken cancellationToken)
    {
        var city = await _repositoryBase.FindByIdAsync(request.Id) ?? throw new CityNotFoundException(request.Id);
        _repositoryBase.Remove(city);
        return Result.Success();
    }
}
