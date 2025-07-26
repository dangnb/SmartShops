using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Contract.Services.V1.Villages;
using Shop.Persentation.Abtractions;
using static Shop.Contract.Services.V1.Villages.Command;

namespace Shop.Persentation.Controllers.V1;
[ApiVersion(1)]
public class VillagesController : ApiController
{
    public VillagesController(ISender sender) : base(sender)
    {
    }

    [Authorize]
    [HttpPost("filter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Filter([FromBody] Query.GetVillagesQuery request)
    {
        var result = await sender.Send(request);
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

    [Authorize]
    [HttpGet("getbywardid/{wardId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByWardId(int wardId)
    {
        var result = await sender.Send(new Query.GetVillagesByWardIdQuery(wardId));
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
    public async Task<IActionResult> GetById(int id)
    {
        var result = await sender.Send(new Query.GetVillageByIdQuery(id));
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
    public async Task<IActionResult> Create([FromBody] Command.CreateVillageCommand createCityCommand)
    {
        var result = await sender.Send(createCityCommand);
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
    public async Task<IActionResult> Update([FromBody] Command.UpdateVillageCommand request, int id)
    {
        var result = await sender.Send(new Command.UpdateVillageCommand(id, request.WardId, request.Code, request.Name, request.username));
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
    public async Task<IActionResult> Delete(int id)
    {
        var result = await sender.Send(new Command.DeleteVillageCommand(id));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

    [Authorize]
    [HttpPut("addusername/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddUsername([FromBody] AddUserNameToVillageCommand request, int id)
    {
        var result = await sender.Send(new AddUserNameToVillageCommand(id, request.username));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }
}
