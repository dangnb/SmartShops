using Shop.Domain.Abstractions;

namespace Shop.Domain.Entities;

public class Ward : EntityAuditBase<Guid>
{
    public Guid ProvincyId { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public Provincy Provincy { get; set; } = default!;

    protected Ward() { }
    private Ward(string code, string name, Guid provincyId)
    {
        Code = code;
        Name = name;
        ProvincyId = provincyId;
    }
    public static Ward CreateEntity(string code, string name, Guid provincyId)
    {
        return new Ward(code, name, provincyId);
    }
    public void Update(string code, string name)
    {
        Code = code;
        Name = name;
    }
}
