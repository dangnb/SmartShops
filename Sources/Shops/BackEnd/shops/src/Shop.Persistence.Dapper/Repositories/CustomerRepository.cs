using System.Data;
using Shop.Domain.Dappers.Repositories;

namespace Shop.Persistence.Dapper.Repositories;
public class CustomerRepository : GenericRepository<Domain.Entities.Customer>, ICustomerRepository
{
    public CustomerRepository(IDbConnection connection) : base(connection)
    {
    }
}
