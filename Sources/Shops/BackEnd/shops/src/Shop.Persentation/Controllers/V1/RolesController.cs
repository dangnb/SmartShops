
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Persentation.Abtractions;
using static Shop.Contract.Services.V1.Roles.Command;
using static Shop.Contract.Services.V1.Roles.Query;

namespace Shop.Persentation.Controllers.V1;
[ApiVersion(1)]
public class RolesController(ISender sender) : ApiController(sender)
{

    [Authorize]
    [HttpGet("get-list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetList()
    {
        var result = await sender.Send(new GetRolesQuery());
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

    [Authorize]
    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand createRoleCommand)
    {
        var result = await sender.Send(createRoleCommand);
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }


    [Authorize]
    [HttpPut("{roleId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommand createRoleCommand, Guid roleId)
    {
        var result = await sender.Send(new UpdateRoleCommand(roleId, createRoleCommand.Name, createRoleCommand.Description, createRoleCommand.PermissionCodes));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{roleId}", Name = "GetByIdRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdRole(Guid roleId)
    {
        var result = await sender.Send(new GetRoleByIdQuery(roleId));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{roleId}", Name = "DeleteRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRole(Guid roleId)
    {
        var result = await sender.Send(new DeleteRoleCommand(roleId));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }
}
