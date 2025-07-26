using System.Data;
using Shop.Domain.Dappers.Repositories;

namespace Shop.Persistence.Dapper.Repositories;
public class ProductRepository : GenericRepository<Domain.Entities.Product>, IProductRepository
{
    public ProductRepository(IDbConnection connection) : base(connection)
    {
    }
}
