using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Products;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.SuppliersException;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Products;

public class DeleteProductCommandHandler : ICommandHandler<Command.DeleteProductCommand>
{
    private readonly IRepositoryBase<Product, Guid> _repositoryBase;

    public DeleteProductCommandHandler(
        IRepositoryBase<Product, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public async Task<Result> Handle(Command.DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await _repositoryBase
            .FindSingleAsync(x => x.Id == request.Id, cancellationToken) ?? throw new SupplierNotFoundException(request.Id);
        product.SoftDelete();
        return Result.Success();
    }
}
