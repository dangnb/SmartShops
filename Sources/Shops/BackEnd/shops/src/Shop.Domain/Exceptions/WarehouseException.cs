using Microsoft.EntityFrameworkCore.Metadata;

namespace Shop.Domain.Exceptions;

public class WarehouseException
{
    public class WarehouseNotFoundException : NotFoundException
    {
        public WarehouseNotFoundException(Guid id)
            : base($"The Warehouse with the id {id} was not found.") { }
    }

    public class DuplicateCodeException : BadRequestException
    {
        public DuplicateCodeException(string code)
            : base($"Mã {code} đã tồn tài rên hệ thống.") { }
    }

}
