using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Products;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.ProductsException;

namespace Shop.Apptication.UserCases.V1.Commands.Products;
public class DeleteProductCommandHandler : ICommandHandler<Command.DeleteProductCommand>
{
    private readonly IRepositoryBase<Product, int> _repositoryBase;
    public DeleteProductCommandHandler(IRepositoryBase<Product, int> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var ward = await _repositoryBase.FindByIdAsync(request.Id)
            ?? throw new ProductNotFoundException(request.Id);
        _repositoryBase.Remove(ward);
        return Result.Success();
    }
}
