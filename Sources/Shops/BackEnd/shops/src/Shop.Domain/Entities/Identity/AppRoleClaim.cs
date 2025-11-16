using Microsoft.AspNetCore.Identity;
using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities.Identity;
public class AppRoleClaim : IdentityRoleClaim<Guid>, ICompanyScopedEntity
{
    public Guid ComId { get;  set; }
}
