using System.Data;
using Shop.Domain.Dappers.Repositories;

namespace Shop.Persistence.Dapper.Repositories;
public class PaymentRepository : GenericRepository<Domain.Entities.Payment>, IPaymentRepository
{
    public PaymentRepository(IDbConnection connection) : base(connection)
    {
    }
}
