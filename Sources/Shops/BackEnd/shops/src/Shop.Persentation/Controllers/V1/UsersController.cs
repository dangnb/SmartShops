using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Versioning;
using HRM.Persentation.Controllers.V1;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.Contract;
using Shop.Persentation.Abtractions;
using static Shop.Contract.Services.V1.Users.Command;
using static Shop.Contract.Services.V1.Users.Query;

namespace Shop.Persentation.Controllers.V1;
[ApiVersion(1)]
public class UsersController : ApiController
{
    private readonly ILogger<UsersController> _logger;

    public UsersController(ISender sender, ILogger<UsersController> logger) : base(sender: sender)
    {
        _logger = logger;
    }

    [Authorize]
    [HttpPost("filter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Filter([FromBody] GetUsersQuery request)
    {
        var result = await sender.Send(request);
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await sender.Send(new GetUserByIdQuery(id));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

    [Authorize]
    [HttpGet("getusers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsers()
    {
        var result = await sender.Send(new GetAllUserQuery());
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }


    //[Authorize]
    [HttpPost(Name = "CreateUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand createUserCommand)
    {
        var result = await sender.Send(createUserCommand);
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand, Guid id)
    {
        var result = await sender.Send(new UpdateUserCommand(id, updateUserCommand.UserName, updateUserCommand.FullName, updateUserCommand.Address, updateUserCommand.RoleCodes));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await sender.Send(new DeleteUserCommand(id));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

    [Authorize]
    [HttpPut("adddistrictforuser/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddDistrictForUser([FromBody] AddDistrictForUserCommand addDistrictForUserCommand, Guid id)
    {
        var result = await sender.Send(new AddDistrictForUserCommand(id, addDistrictForUserCommand.DistrictCodes));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

    [Authorize]
    [HttpGet("getdistrictcode/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDistrictCode(Guid id)
    {
        var result = await sender.Send(new GetDistrictCodeQuery(id));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }
}
