
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Contract.Services.V1.Districts;
using Shop.Persentation.Abtractions;
using static Shop.Contract.Services.V1.Cities.Query;
using static Shop.Contract.Services.V1.Districts.Query;

namespace Shop.Persentation.Controllers.V1;
[ApiVersion(1)]
public class DistrictsController(ISender sender) : ApiController(sender)
{
    [Authorize]
    [HttpPost("filter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Filter([FromBody] Query.GetDistrictsQuery request)
    {
        var result = await sender.Send(request);
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }


    [Authorize]
    [HttpGet("getbycity/{cityId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCity(int cityId)
    {
        var result = await sender.Send(new GetDistrictsByCityQuery(cityId));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

    [Authorize]
    [HttpGet("getbyuser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByUser(int cityId)
    {
        var result = await sender.Send(new GetDistrictsByCityQuery(cityId));
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
        var result = await sender.Send(new Query.GetDistrictByIdQuery(id));
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
    public async Task<IActionResult> Create([FromBody] Command.CreateDistrictCommand createCityCommand)
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
    public async Task<IActionResult> Update([FromBody] Command.UpdateDistrictCommand request, int id)
    {
        var result = await sender.Send(new Command.UpdateDistrictCommand(id, request.CityId, request.Code, request.Name));
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
        var result = await sender.Send(new Command.DeleteDistrictCommand(id));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }
}
