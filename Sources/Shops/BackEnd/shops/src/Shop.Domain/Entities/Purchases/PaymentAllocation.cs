namespace Shop.Domain.Entities.Purchases;

public class PaymentAllocation // ❌ bỏ sealed
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid PaymentId { get; private set; }
    public Guid InvoiceId { get; private set; }
    public decimal AmountAllocated { get; private set; }

    protected PaymentAllocation() { } // ✅ cho EF

    internal PaymentAllocation(Guid invoiceId, decimal amount)
    {
        InvoiceId = invoiceId;
        AmountAllocated = amount;
    }
}
