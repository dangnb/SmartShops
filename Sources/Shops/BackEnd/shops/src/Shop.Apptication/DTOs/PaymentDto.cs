namespace Shop.Apptication.DTOs;

public record AllocatePaymentBody(Guid InvoiceId, decimal Amount);
