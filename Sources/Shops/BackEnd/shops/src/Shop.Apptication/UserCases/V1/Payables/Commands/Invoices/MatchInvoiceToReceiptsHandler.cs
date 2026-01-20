using Microsoft.EntityFrameworkCore;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.Purchasing.V1.Invoices;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.enums;
using Shop.Domain.Entities.Purchases;

namespace Shop.Apptication.UserCases.V1.Payables.Commands.Invoices;

public sealed class MatchInvoiceToReceiptsHandler : ICommandHandler<Command.MatchInvoiceToReceiptsCommand>
{
    private readonly IRepositoryBase<PurchaseInvoice, Guid> _repositoryBase;
    private readonly IRepositoryBase<GoodsReceipt, Guid> _repositoryGoodReceipt;
    public MatchInvoiceToReceiptsHandler(IRepositoryBase<PurchaseInvoice, Guid> repositoryBase, IRepositoryBase<GoodsReceipt, Guid> repositoryGoodReceipt)
    {
        _repositoryBase = repositoryBase;
        _repositoryGoodReceipt = repositoryGoodReceipt;
    }
    public async Task<Result> Handle(Command.MatchInvoiceToReceiptsCommand req, CancellationToken cancellationToken)
    {
        PurchaseInvoice invoice = await _repositoryBase.FindSingleAsync(ct => ct.Id == req.InvoiceId, cancellationToken, x => x.Lines)
            ?? throw new Exception("Invoice not found.");

        if (invoice.Status != DocumentStatus.Issued)
            throw new Exception("Invoice must be Issued before matching.");

        foreach (var item in req.Items)
        {
            // 1) invoice remaining by product
            var invLineQty = invoice.Lines.Where(l => l.ProductId == item.ProductId).Sum(l => l.Qty);
            var invMatchedQty = invoice.Matches.Where(m => m.ProductId == item.ProductId).Sum(m => m.QtyMatched);
            var invRemaining = invLineQty - invMatchedQty;
            if (item.QtyMatched > invRemaining)
                throw new Exception($"Invoice qty remaining not enough for product {item.ProductId}.");

            // 2) receipt remaining by product (across ALL invoices)
            var receipt = await _repositoryGoodReceipt.FindSingleAsync(r => r.Id == item.ReceiptId, cancellationToken, x => x.Lines)
                ?? throw new Exception("Receipt not found.");

            if (receipt.SupplierId != invoice.SupplierId)
                throw new Exception("Supplier mismatch between invoice and receipt.");

            var recQty = receipt.Lines.Where(l => l.ProductId == item.ProductId).Sum(l => l.Qty);

            var recMatchedQty = await _repositoryBase.FindAll()
                .SelectMany(pi => pi.Matches)
                .Where(m => m.ReceiptId == item.ReceiptId && m.ProductId == item.ProductId)
                .SumAsync(m => m.QtyMatched, cancellationToken);

            var recRemaining = recQty - recMatchedQty;
            if (item.QtyMatched > recRemaining)
                throw new Exception($"Receipt qty remaining not enough for product {item.ProductId}.");

            invoice.AddMatch(item.ReceiptId, item.ReceiptLineId, item.ProductId, item.QtyMatched, item.AmountMatched);
            _repositoryBase.Update(invoice);
        }
        return Result.Success();
    }
}
