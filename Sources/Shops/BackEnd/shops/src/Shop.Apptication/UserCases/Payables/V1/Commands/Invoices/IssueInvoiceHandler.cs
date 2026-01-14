using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.Purchasing.V1.Invoices;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Purchases;

namespace Shop.Apptication.UserCases.Payables.V1.Commands.Invoices;

public sealed class IssueInvoiceHandler : ICommandHandler<Command.IssueInvoiceCommand>
{
    private readonly IRepositoryBase<PurchaseInvoice, Guid> _repositoryBase;
    public IssueInvoiceHandler(IRepositoryBase<PurchaseInvoice, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.IssueInvoiceCommand req, CancellationToken cancellationToken)
    {
        PurchaseInvoice invoice = await _repositoryBase.FindSingleAsync(ct => ct.Id == req.InvoiceId, cancellationToken, x => x.Lines)
            ?? throw new Exception("Invoice not found.");
        invoice.Issue();
        _repositoryBase.Update(invoice);
        return Result.Success(invoice);
    }
}
