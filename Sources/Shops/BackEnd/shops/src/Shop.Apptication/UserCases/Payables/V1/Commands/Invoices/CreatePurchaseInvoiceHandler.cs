using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.Purchasing.V1.Invoices;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Purchases;

namespace Shop.Apptication.UserCases.Payables.V1.Commands.Invoices;

public sealed class CreatePurchaseInvoiceHandler : ICommandHandler<Command.CreatePurchaseInvoiceCommand>
{
    private readonly IRepositoryBase<PurchaseInvoice, Guid> _repositoryBase;
    public CreatePurchaseInvoiceHandler(IRepositoryBase<PurchaseInvoice, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async  Task<Result> Handle(Command.CreatePurchaseInvoiceCommand req, CancellationToken cancellationToken)
    {
        var invoice = new PurchaseInvoice(req.SupplierId, req.InvoiceNo, req.InvoiceDate, req.DueDate);
        _repositoryBase.Add(invoice);
        return Result.Success(invoice);
    }
}
