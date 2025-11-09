using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Shop.Apptication.Exceptions;
public class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor _context;

    public UserProvider(IHttpContextAccessor context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    public Guid GetComID()
    {
        Guid.TryParse(_context.HttpContext.User.Claims
                    .First(i => i.Type == ClaimTypes.GroupSid).Value, out Guid _comId);
        return _comId;
    }

    public string GetTaxCode()
    {
        return _context.HttpContext.User.Claims
                  .First(i => i.Type == ClaimTypes.Sid).Value;
    }

    string IUserProvider.GetUserName()
    {
        return _context.HttpContext.User.Claims
                   .First(i => i.Type == ClaimTypes.Name).Value;
    }
}
