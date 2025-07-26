namespace Shop.Domain.Exceptions;
public class PaymentException : BadRequestException
{
    public PaymentException(string message) : base(message)
    {
    }
}
