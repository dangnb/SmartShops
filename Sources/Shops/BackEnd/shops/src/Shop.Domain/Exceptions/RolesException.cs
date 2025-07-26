namespace Shop.Domain.Exceptions;
public static class RolesException
{
    public class RoleNotFoundException : NotFoundException
    {
        public RoleNotFoundException(Guid id)
            : base($"The role with the id {id} was not found.") { }
    }
}
