namespace Shop.Domain.Exceptions;
public static class DistrictsException
{
    public class DistrictNotFoundException : NotFoundException
    {
        public DistrictNotFoundException(int id)
            : base($"The District with the id {id} was not found.") { }
    }
}
