namespace Shop.Domain.Exceptions;
public static class SuppliersException
{
    public class SupplierNotFoundException : NotFoundException
    {
        public SupplierNotFoundException(Guid id)
            : base($"The Supplier with the id {id} was not found.") { }
    }
}
