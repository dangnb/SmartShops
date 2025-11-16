namespace Shop.Domain.Entities;

public class Ward
{
    public Guid Id { get; set; }
    public Guid ProvincyId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}
