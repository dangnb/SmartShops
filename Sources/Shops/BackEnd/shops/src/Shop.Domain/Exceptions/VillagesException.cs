namespace Shop.Domain.Exceptions;
public static class VillagesException
{
    public class VillageNotFoundException : NotFoundException
    {
        public VillageNotFoundException(long id)
            : base($"The village with the id {id} was not found.") { }
    }
}
