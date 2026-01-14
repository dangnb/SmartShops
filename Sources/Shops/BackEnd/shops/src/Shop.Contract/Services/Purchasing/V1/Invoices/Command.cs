using MediatR;
using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.Purchasing.V1.Invoices;

public class Command
{
    public sealed record CreatePurchaseInvoiceCommand(Guid SupplierId, string InvoiceNo, DateOnly InvoiceDate, DateOnly? DueDate) : ICommand;
    public sealed record AddInvoiceLineCommand(Guid InvoiceId, Guid ProductId, decimal Qty, decimal UnitPrice, decimal TaxRate) : ICommand;
    public sealed record IssueInvoiceCommand(Guid InvoiceId) : ICommand;
    public sealed record MatchItem(Guid ReceiptId, Guid ProductId, decimal QtyMatched, decimal AmountMatched, Guid? ReceiptLineId);
    public sealed record MatchInvoiceToReceiptsCommand(Guid InvoiceId, List<MatchItem> Items) : ICommand;
}
