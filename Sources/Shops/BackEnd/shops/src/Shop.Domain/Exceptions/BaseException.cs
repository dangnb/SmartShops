namespace Shop.Domain.Exceptions;

public class DomainBaseException : DomainException
{
    public DomainBaseException(string message) : base("DomainException", message)
    {
    }
}
