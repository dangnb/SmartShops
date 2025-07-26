using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities;
public class Config : DomainEntity<int>
{
    public int ComId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;

    protected Config() { }

    private Config(int comId, string code, string value)
    {
        Code = code;
        Value = value;
        ComId = comId;
    }
    public static Config CreateEntity(int comId, string code, string value)
    {
        return new Config(comId, code, value);
    }

    public void Update(string code, string value)
    {
        Code = code;
        Value = value;
    }
}
