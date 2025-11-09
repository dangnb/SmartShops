using Shop.Domain.Abstractions;
using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities;
public class Config : DomainEntity<int>, ICompanyScopedEntity
{
    public string Code { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;

    public Guid ComId { get; set; }

    protected Config() { }

    private Config(Guid comId, string code, string value)
    {
        Code = code;
        Value = value;
        ComId = comId;
    }
    public static Config CreateEntity(Guid comId, string code, string value)
    {
        return new Config(comId, code, value);
    }

    public void Update(string code, string value)
    {
        Code = code;
        Value = value;
    }
}
