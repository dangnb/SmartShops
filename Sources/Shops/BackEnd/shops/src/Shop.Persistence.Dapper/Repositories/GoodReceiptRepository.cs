using System.Data;
using Shop.Domain.Dappers.Repositories;
using Shop.Domain.Entities.Purchases;

namespace Shop.Persistence.Dapper.Repositories;

public class GoodReceiptRepository : GenericRepository<GoodsReceipt>, IGoodReceiptRepository
{
    public GoodReceiptRepository(IDbConnection connection) : base(connection)
    {
    }
}
