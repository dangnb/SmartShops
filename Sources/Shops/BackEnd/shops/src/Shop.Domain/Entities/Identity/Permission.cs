using System.Text.Json.Serialization;
using System.Xml.Linq;
using Shop.Domain.Abstractions.Entities;
using Shop.Domain.Entities.Metadata;

namespace Shop.Domain.Entities.Identity;
public class Permission : DomainEntity<Guid>
{
    public string Description { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string GroupCode { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;

    [JsonIgnore]
    public virtual ICollection<AppRole> AppRoles { get; set; }
    protected Permission() { }
    private Permission(string code, string description, string groupCode, string groupName)
    {
        Id= Guid.NewGuid();
        Code = code;
        Description = description;
        GroupCode = groupCode;
        GroupName = groupName;
    }

    public static Permission CreateEntity(string code, string description, string groupCode, string groupName) { 
        return new Permission( code,  description,  groupCode,  groupName); }

    public void Update(string code, string description, string groupCode, string groupName) { 
        Code = code; 
        Description = description;
        GroupCode = groupCode;
        GroupName = groupName;
    }
}
