using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Shop.Contract;
using Shop.Contract.Extensions;

namespace Shop.Infrastructure;

public class CurrentUser(IHttpContextAccessor accessor) : ICurrentUser
{
    private readonly ClaimsPrincipal? _user = accessor.HttpContext?.User;

    public Guid? UserId =>
        Guid.TryParse(_user?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id) ? id : null;

    public Guid? ComId =>
        Guid.TryParse(_user?.FindFirst(CustomClaimTypes.ComId)?.Value, out var id) ? id : null;


    public bool TryGet(string claimType, out string? value)
    {
        value = _user?.FindFirst(claimType)?.Value;
        return value is not null;
    }

    public Guid GetRequiredCompanyId()
        => ComId ?? throw new InvalidOperationException("Missing company context (comid claim).");
}

