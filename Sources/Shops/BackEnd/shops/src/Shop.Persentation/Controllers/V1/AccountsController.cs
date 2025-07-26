using Asp.Versioning;
using Shop.Contract;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Enumerations;
using Shop.Contract.Extensions.Products;
using Shop.Contract.Services.V1.Users;
using Shop.Persentation.Abtractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRM.Persentation.Controllers.V1;

[ApiVersion(1)]
public class AccountsController : ApiController
{
    private readonly ILogger<AccountsController> _logger;

    public AccountsController(ISender sender, ILogger<AccountsController> logger) : base(sender: sender)
    {
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost("login", Name = "login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody] Command.LoginCommand loginCommand)
    {
        var result = await sender.Send(loginCommand);
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }
    [Authorize]
    [HttpGet("getuserbytoken", Name = "getuserbytoken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserByToken()
    {
        var result = await sender.Send(new Query.GetUserByTokenQuery());
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

}
