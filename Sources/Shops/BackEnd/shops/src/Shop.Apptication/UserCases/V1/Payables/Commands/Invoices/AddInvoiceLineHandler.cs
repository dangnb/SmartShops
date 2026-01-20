using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Purchasing.Invoices;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Purchases;

namespace Shop.Apptication.UserCases.V1.Payables.Commands.Invoices;

public sealed class AddInvoiceLineHandler : ICommandHandler<Command.AddInvoiceLineCommand>
{
    private readonly IRepositoryBase<PurchaseInvoice, Guid> _repositoryBase;
    public AddInvoiceLineHandler(IRepositoryBase<PurchaseInvoice, Guid> repositoryBase) => _repositoryBase = repositoryBase;
    public async Task<Result> Handle(Command.AddInvoiceLineCommand req, CancellationToken cancellationToken)
    {
        PurchaseInvoice invoice = await _repositoryBase.FindSingleAsync(ct => ct.Id == req.InvoiceId, cancellationToken, x => x.Lines)
            ?? throw new Exception("Invoice not found.");
        invoice.AddLine(req.ProductId, req.Qty, req.UnitPrice, req.TaxRate);
        _repositoryBase.Update(invoice);
        return Result.Success(invoice);
    }
}
