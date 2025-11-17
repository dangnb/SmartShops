using Shop.Domain.Abstractions;
using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities;

public class Ward : DomainEntity<Guid>, ICompanyScopedEntity
{
    public Guid ComId { get; set; } = default!;
    public Guid ProvincyId { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;

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
