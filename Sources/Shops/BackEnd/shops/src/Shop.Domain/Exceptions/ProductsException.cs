namespace Shop.Domain.Exceptions;
public static class ProductsException
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(int id)
            : base($"The Product with the id {id} was not found.") { }
    }
}
