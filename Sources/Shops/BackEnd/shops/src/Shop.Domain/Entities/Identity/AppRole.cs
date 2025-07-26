using Microsoft.AspNetCore.Identity;

namespace Shop.Domain.Entities.Identity;
public class AppRole : IdentityRole<Guid>
{
    public string Description { get; private set; } = string.Empty;
    public int ComId { get; private set; }
    public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }
    public virtual ICollection<AppRoleClaim> Claims { get; set; }
    public virtual ICollection<Permission> Permissions { get; set; }
    protected AppRole() { }
    public AppRole(Guid id) { Id = id; }
    private AppRole(int comId, string description, string name)
    {
        ComId = comId;
        Description = description;
        Name = name;
    }

    public static AppRole CreateEntity(int comId, string description, string name)
    {
        return new AppRole(comId, description, name);
    }

    public void UpdateEntity(string description, string name)
    {
        Description = description;
        Name = name;
    }

}
