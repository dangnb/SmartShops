using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Common.Products;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.ProductsException;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Products;

public class UpdateProductCommandHandler : ICommandHandler<Command.UpdateProductCommand>
{

    private readonly IRepositoryBase<Product, Guid> _repositoryBase;
    public UpdateProductCommandHandler(IRepositoryBase<Product, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repositoryBase.FindSingleAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new ProductNotFoundException();

        product.Update(
            name: request.Name,
            barCode: request.BarCode,
            categoryId: request.CategoryId
        );
        _repositoryBase.Update(product);
        return Result.Success();
    }
}
