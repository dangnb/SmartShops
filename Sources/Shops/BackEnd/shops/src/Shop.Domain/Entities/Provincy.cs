using Shop.Domain.Abstractions;
using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities;

public class Provincy : DomainEntity<Guid>, ICompanyScopedEntity
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public Guid ComId { get; set; } = default!;
    protected Provincy() { }
    private Provincy(string code, string name)
    {
        Code = code;
        Name = name;
    }
    public static Provincy CreateEntity(string code, string name)
    {
        return new Provincy(code, name);
    }
    public void Update(string code, string name)
    {
        Code = code;
        Name = name;
    }
}
