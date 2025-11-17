using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Shop.Contract;
using Shop.Contract.Extensions;

namespace Shop.Infrastructure;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _accessor;

    public CurrentUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    private ClaimsPrincipal? User => _accessor.HttpContext?.User;

    public string? UserId =>
        User?.FindFirst(ClaimTypes.Name)?.Value;

    public Guid? ComId =>
        Guid.TryParse(User?.FindFirst(CustomClaimTypes.ComId)?.Value, out var id) ? id : null;

    public bool TryGet(string claimType, out string? value)
    {
        value = User?.FindFirst(claimType)?.Value;
        return value is not null;
    }

    public Guid GetRequiredCompanyId()
        => ComId ?? throw new InvalidOperationException("Missing company context (comid claim).");
}
