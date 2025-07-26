using System.Data;
using Shop.Domain.Dappers.Repositories;

namespace Shop.Persistence.Dapper.Repositories;
public class DistrictRepository : GenericRepository<Domain.Entities.Metadata.District>, IDistrictRepository
{
    public DistrictRepository(IDbConnection connection) : base(connection)
    {
    }
}
