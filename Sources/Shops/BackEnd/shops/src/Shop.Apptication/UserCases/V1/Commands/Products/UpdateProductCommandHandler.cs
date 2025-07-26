using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Products;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Metadata;
using static Shop.Domain.Exceptions.ProductsException;
using static Shop.Domain.Exceptions.VillagesException;
using static Shop.Domain.Exceptions.WardsException;

namespace Shop.Apptication.UserCases.V1.Commands.Products;
public class UpdateProductCommandHandler : ICommandHandler<Command.UpdateProductCommand>
{
    private readonly IRepositoryBase<Product, int> _repositoryBase;
    private readonly IUserProvider _userProvider;
    public UpdateProductCommandHandler(IRepositoryBase<Product, int> repositoryBase, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _userProvider = userProvider;
    }
    public async Task<Result> Handle(Command.UpdateProductCommand request, CancellationToken cancellationToken)
    {

        var product = await _repositoryBase.FindByIdAsync(request.Id) 
            ?? throw new ProductNotFoundException(request.Id);
        product.Update(request.Name, request.Code,  request.Price, request.IsActive, request.ProductType);
        _repositoryBase.Update(product);
        return Result.Success(product);
    }
}
