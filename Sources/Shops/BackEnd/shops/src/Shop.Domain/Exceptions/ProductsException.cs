namespace Shop.Domain.Exceptions;
public static class ProductsException
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException()
            : base($"Không tìm thấy thông tin sản phẩm") { }
    }
}
