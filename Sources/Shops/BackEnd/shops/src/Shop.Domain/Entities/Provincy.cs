using Shop.Domain.Abstractions;

namespace Shop.Domain.Entities;

public class Province : EntityAuditBase<Guid>
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    protected Province() { }
    private Province(string code, string name)
    {
        Code = code;
        Name = name;
    }
    public static Province CreateEntity(string code, string name)
    {
        return new Province(code, name);
    }
    public void Update(string code, string name)
    {
        Code = code;
        Name = name;
    }
}
