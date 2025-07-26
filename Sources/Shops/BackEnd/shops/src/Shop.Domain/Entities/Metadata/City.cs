using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities.Metadata;
public class City : DomainEntity<int>
{
    public int ComId { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;

    protected City() { }
    private City(int comId, string code, string name)
    {
        ComId = comId;
        Code = code;
        Name = name;
    }

    public static City CreateEntity(int comId, string code, string name) { return new City(comId, code, name); }

    public void Update(string code, string name) { Code = code; Name = name; }
}
