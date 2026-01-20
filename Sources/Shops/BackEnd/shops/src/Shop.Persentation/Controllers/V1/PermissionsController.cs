using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Persentation.Abtractions;
using Shop.Contract.Services.V1.Common.Permissions;

namespace Shop.Persentation.Controllers.V1;
[ApiVersion(1)]
public class PermissionsController : ApiController
{
    public PermissionsController(ISender sender) : base(sender)
    {
    }

    [Authorize]
    [HttpPost("filter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Filter([FromBody] Query.GetPermissionsQuery request)
    {
        var result = await sender.Send(request);
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

    [Authorize]
    [HttpGet("get-list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetList()
    {
        var result = await sender.Send(new Query.GetAllPermissionsQuery());
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }



    [Authorize]
    [HttpGet("{permissionid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid permissionid)
    {
        var result = await sender.Send(new Query.GetPermissionByIdQuery(permissionid));
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
    public async Task<IActionResult> Create([FromBody] Command.CreatePermissionCommand createPermissionCommand)
    {
        var result = await sender.Send(createPermissionCommand);
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

    [Authorize]
    [HttpPut("{permissionid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] Command.UpdatePermissionCommand updatePermissionCommand, Guid permissionid)
    {
        
        var result = await sender.Send(new Command.UpdatePermissionCommand(permissionid, updatePermissionCommand.Description, updatePermissionCommand.Code, updatePermissionCommand.GroupCode, updatePermissionCommand.GroupName));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{permissionid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid permissionid)
    {
        var result = await sender.Send(new Command.DeletePermissionCommand(permissionid));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }
}
