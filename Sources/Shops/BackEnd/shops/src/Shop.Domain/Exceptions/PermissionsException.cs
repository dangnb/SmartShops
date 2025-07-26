namespace Shop.Domain.Exceptions;
public static class PermissionsException
{
    public class PermissionNotFoundException : NotFoundException
    {
        public PermissionNotFoundException(Guid id)
            : base($"The Permission with the id {id} was not found.") { }
    }
}
