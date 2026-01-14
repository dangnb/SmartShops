using Microsoft.EntityFrameworkCore;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.Payables.V1.Payments;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Purchases;

namespace Shop.Apptication.UserCases.Payments.V1.Commands;

internal class AllocatePaymentHandler : ICommandHandler<Command.AllocatePaymentCommand>
{
    private readonly IRepositoryBase<Payment, Guid> _repositoryBase;
    private readonly IRepositoryBase<PurchaseInvoice, Guid> _repositoryPurchase;
    public AllocatePaymentHandler(IRepositoryBase<Payment, Guid> repositoryBase, IRepositoryBase<PurchaseInvoice, Guid> repositoryPurchase
        )
    {
        _repositoryBase = repositoryBase;
        _repositoryPurchase = repositoryPurchase;
    }
    public async Task<Result> Handle(Command.AllocatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _repositoryBase.FindSingleAsync(p => p.Id == request.PaymentId, cancellationToken, p => p.Allocations)
            ?? throw new Exception("Payment not found.");

        var invoice = await _repositoryPurchase.FindSingleAsync(i => i.Id == request.InvoiceId, cancellationToken)
            ?? throw new Exception("Invoice not found.");

        if (payment.SupplierId != invoice.SupplierId)
            throw new Exception("Supplier mismatch between payment and invoice.");

        // invoice outstanding = Total - sum allocations across all payments
        var paidOnInvoice = await _repositoryBase.FindAll()
            .SelectMany(p => p.Allocations)
            .Where(a => a.InvoiceId == invoice.Id)
            .SumAsync(a => a.AmountAllocated, cancellationToken);

        var outstanding = invoice.Total - paidOnInvoice;
        if (request.Amount > outstanding)
            throw new Exception("Allocation exceeds invoice outstanding.");

        payment.Allocate(invoice.Id, request.Amount);
        return Result.Success(payment);
    }
}
