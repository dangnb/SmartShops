using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Contract.Services.V1.Wards;
using Shop.Persentation.Abtractions;
using static Shop.Contract.Services.V1.Districts.Query;
using static Shop.Contract.Services.V1.Wards.Query;

namespace Shop.Persentation.Controllers.V1;
[ApiVersion(1)]
public class WardsController : ApiController
{
    public WardsController(ISender sender) : base(sender)
    {
    }

    [Authorize]
    [HttpPost("filter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Filter([FromBody] Query.GetWardsQuery request)
    {
        var result = await sender.Send(request);
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

    [Authorize]
    [HttpGet("getbydistrict/{districtId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCity(int districtId)
    {
        var result = await sender.Send(new GetWardByDistrictQuery(districtId));
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
        var result = await sender.Send(new Query.GetWardByIdQuery(id));
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
    public async Task<IActionResult> Create([FromBody] Command.CreateWardCommand createCityCommand)
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
    public async Task<IActionResult> Update([FromBody] Command.UpdateWardCommand request, int id)
    {
        var result = await sender.Send(new Command.UpdateWardCommand(id, request.DistrictId, request.Code, request.Name));
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
        var result = await sender.Send(new Command.DeleteWardCommand(id));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }
}
