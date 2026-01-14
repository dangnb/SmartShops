using System.Data;
using Shop.Domain.Dappers.Repositories;
using Shop.Domain.Entities.Purchases;

namespace Shop.Persistence.Dapper.Repositories;
public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(IDbConnection connection) : base(connection)
    {
    }
}
