using Shop.Domain.Abstractions;
using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities;
public class Payment : EntityAuditBase<Guid>
{
    public Guid CustomerId { get; private set; }
    public decimal AmountPaid { get; private set; }
}
