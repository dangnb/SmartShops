using Shop.Domain.Dappers.Repositories;

namespace Shop.Domain.Dappers;
public interface IUnitOfWork
{
    IProductRepository Products { get; }
}
