using System.Data;
using Shop.Domain.Dappers.Repositories;

namespace Shop.Persistence.Dapper.Repositories;
public class UserRepository : GenericRepository<Domain.Entities.Identity.AppUser>, IUserRepository
{
    public UserRepository(IDbConnection connection) : base(connection)
    {
    }
}
