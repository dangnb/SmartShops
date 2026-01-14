using Shop.Domain.Abstractions;
using Shop.Domain.Exceptions;

namespace Shop.Domain.Entities.Purchases;

public sealed class Payment : EntityAuditBase<Guid>
{
    private readonly List<PaymentAllocation> _allocations = new();

    public string PaymentNo { get; private set; } = default!;
    public Guid SupplierId { get; private set; }
    public DateOnly PaymentDate { get; private set; }
    public decimal Amount { get; private set; }
    public string Method { get; private set; } = "cash"; // cash/bank
    public string? Reference { get; private set; }

    public IReadOnlyCollection<PaymentAllocation> Allocations => _allocations.AsReadOnly();

    private Payment() { }

    public Payment(string paymentNo, Guid supplierId, DateOnly paymentDate, decimal amount, string method, string? reference)
    {
        if (string.IsNullOrWhiteSpace(paymentNo))
            throw new DomainBaseException("PaymentNo is required.");
        if (amount <= 0)
            throw new DomainBaseException("Amount must be > 0.");
        PaymentNo = paymentNo.Trim();
        SupplierId = supplierId;
        PaymentDate = paymentDate;
        Amount = amount;
        Method = string.IsNullOrWhiteSpace(method) ? "cash" : method.Trim().ToLowerInvariant();
        Reference = reference;
    }

    public decimal AllocatedAmount => _allocations.Sum(x => x.AmountAllocated);
    public decimal UnallocatedAmount => Amount - AllocatedAmount;

    public void Allocate(Guid invoiceId, decimal amount)
    {
        if (amount <= 0)
            throw new DomainBaseException("Allocated amount must be > 0.");
        if (amount > UnallocatedAmount)
            throw new DomainBaseException("Allocated amount exceeds unallocated balance.");
        _allocations.Add(new PaymentAllocation(invoiceId, amount));
    }
}

public sealed class PaymentAllocation
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid PaymentId { get; private set; }
    public Guid InvoiceId { get; private set; }
    public decimal AmountAllocated { get; private set; }

    private PaymentAllocation() { }
    internal PaymentAllocation(Guid invoiceId, decimal amount)
    {
        InvoiceId = invoiceId;
        AmountAllocated = amount;
    }
}
