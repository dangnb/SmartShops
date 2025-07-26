namespace Shop.Domain.Exceptions;
public static class CustomersException
{
    public class CustomerNotFoundException : NotFoundException
    {
        public CustomerNotFoundException(Guid id)
            : base($"The Customer with the id {id} was not found.") { }
    }
}
