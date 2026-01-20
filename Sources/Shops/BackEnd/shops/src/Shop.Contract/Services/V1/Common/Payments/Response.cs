namespace Shop.Contract.Services.V1.Common.Payments;
public class Response
{
    public record PaymentResponse(Guid Id, Guid Code, Guid CustomerId, string CustomerName,string CustomerAddress,  string CustomerCode, int Quantity, string TotalOfMonth, 
        int NumberOfMonths, decimal Price, decimal Total, decimal VatAmount, decimal Amount, int Type, int Status, DateTime CreatedDate, string CreatedBy,
        DateTime? ModifiedDate, string? ModifiedBy, string Note, bool IsPrinted);
}
