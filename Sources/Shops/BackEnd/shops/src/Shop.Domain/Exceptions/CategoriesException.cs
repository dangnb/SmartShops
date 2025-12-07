namespace Shop.Domain.Exceptions;

public static class CategoriesException
{
    public class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException()
            : base($"Không tìm thấy dữ liệu") { }
    }

    public class CategoryNotFoundParentException : NotFoundException
    {
        public CategoryNotFoundParentException()
            : base($"Không tìm thấy dữ liệu cha") { }
    }

    public class CategoryDublicateCodeException : BadRequestException
    {
        public CategoryDublicateCodeException()
            : base($"Mã đã tồn tại trên trên hệ thống") { }

    }
}
