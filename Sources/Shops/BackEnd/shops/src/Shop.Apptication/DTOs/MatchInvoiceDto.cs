namespace Shop.Apptication.DTOs;

public record MatchInvoiceBody(List<MatchInvoiceItemBody> Items);
public record MatchInvoiceItemBody(Guid ReceiptId, Guid ProductId, decimal QtyMatched, decimal AmountMatched, Guid? ReceiptLineId);
