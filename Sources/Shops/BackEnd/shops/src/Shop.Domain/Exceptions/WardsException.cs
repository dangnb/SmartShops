namespace Shop.Domain.Exceptions;
public static class WardsException
{
    public class WardNotFoundException : NotFoundException
    {
        public WardNotFoundException(Guid id)
            : base($"The ward with the id {id} was not found.") { }
    }
}
