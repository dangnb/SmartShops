using MediatR;
using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Payables.Payments;

public class Command
{
    public sealed record RecordPaymentCommand(string PaymentNo, Guid SupplierId, DateOnly PaymentDate, decimal Amount, string Method, string? Reference) : ICommand;
    public sealed record AllocatePaymentCommand(Guid PaymentId, Guid InvoiceId, decimal Amount) : ICommand;
}
