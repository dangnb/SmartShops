using Shop.Domain.Abstractions;

namespace Shop.Domain.Entities;

public class Provincy : EntityAuditBase<Guid>
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
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
