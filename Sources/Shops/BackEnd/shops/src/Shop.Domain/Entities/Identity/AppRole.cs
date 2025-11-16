using Microsoft.AspNetCore.Identity;
using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities.Identity;

public class AppRole : IdentityRole<Guid>, ICompanyScopedEntity
{
    public string Description { get; private set; } = string.Empty;
    public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }
    public virtual ICollection<AppRoleClaim> Claims { get; set; }
    public virtual ICollection<Permission> Permissions { get; set; }

    public Guid ComId { get; set; }

    protected AppRole() { }
    public AppRole(Guid id) { Id = id; }
    private AppRole(Guid comId, string description, string name)
    {
        ComId = comId;
        Description = description;
        Name = name;
        UserRoles = new List<IdentityUserRole<Guid>>();
        Claims = new List<AppRoleClaim>();
        Permissions = new List<Permission>();
    }

    public static AppRole CreateEntity(Guid comId, string description, string name)
    {
        return new AppRole(comId, description, name);
    }

    public void UpdateEntity(string description, string name)
    {
        Description = description;
        Name = name;
    }

}
