using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Suppliers;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.SuppliersException;

namespace Shop.Apptication.UserCases.V1.Commands.Suppliers;

public class DeleteSupplierCommandHandler : ICommandHandler<Command.DeleteSupplierCommand>
{
    private readonly IRepositoryBase<Supplier, Guid> _repositoryBase;

    public DeleteSupplierCommandHandler(
        IRepositoryBase<Supplier, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public async Task<Result> Handle(Command.DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _repositoryBase
      .FindSingleAsync(x => x.Id == request.Id, cancellationToken)??   throw new SupplierNotFoundException(request.Id);

        supplier.SoftDelete();

        return Result.Success();
    }
}
