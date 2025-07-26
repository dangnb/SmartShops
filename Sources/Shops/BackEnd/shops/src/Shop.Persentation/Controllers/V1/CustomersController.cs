using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Contract.Services.V1.Customers;
using Shop.Persentation.Abtractions;

namespace Shop.Persentation.Controllers.V1;
[ApiVersion(1)]
public class CustomersController : ApiController
{
    public CustomersController(ISender sender) : base(sender)
    {
    }

    [Authorize]
    [HttpPost("filter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Filter([FromBody] Query.GetCustomersQuery request)
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
        var result = await sender.Send(new Query.GetCustomerByIdQuery(id));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }


    [Authorize]
    [HttpPost("getcustomerbypaid")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerByPaid([FromBody] Query.GetCustomersPaidQuery query)
    {
        var result = await sender.Send(query);
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
    public async Task<IActionResult> Create([FromBody] Command.CreateCustomerCommand createCityCommand)
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
    public async Task<IActionResult> Update([FromBody] Command.UpdateCustomerCommand request, Guid id)
    {
        var result = await sender.Send(new Command.UpdateCustomerCommand(id, request.Code, request.Name, request
            .Address, request.Email, request.PhoneNumber, request.VillageId, request.payments));
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
        var result = await sender.Send(new Command.DeleteCustomerCommand(id));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

    [Authorize]
    [HttpPost("upload")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Upload([FromForm] Command.UploadCustomerCommand request)
    {
        var result = await sender.Send(new Command.UploadCustomerCommand(request.File, request.VillageId));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }
        return Ok(result);
    }
}
