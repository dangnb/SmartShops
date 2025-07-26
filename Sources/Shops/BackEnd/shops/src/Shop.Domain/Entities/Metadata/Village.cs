using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities.Metadata;
public class Village : DomainEntity<int>
{
    public int WardId { get; private set; }
    public int ComId { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string UserName { get; private set; } = string.Empty;//Nhân viên thu
    public virtual Ward Ward { get; private set; }

    protected Village() { }
    private Village(int comId, int wardId, string code, string name, string username)
    {
        ComId = comId;
        Code = code;
        Name = name;
        WardId = wardId;
        UserName = username;
    }

    public static Village CreateEntity(int comId, int wardId, string code, string name, string username) { return new Village(comId, wardId, code, name, username); }

    public void Update(int wardId, string code, string name, string username) { Code = code; Name = name; WardId = wardId; UserName = username; }
    public void AddUserName(string username)
    {
        UserName = username;
    }
}
