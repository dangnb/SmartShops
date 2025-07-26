using Shop.Domain.Dappers;
using Shop.Domain.Dappers.Repositories;

namespace Shop.Persistence.Dapper;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IProductRepository productRepository)
    {
        Products = productRepository;
    }
    public IProductRepository Products { get; }
}
