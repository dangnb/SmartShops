using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Contract.Services.V1.Suppliers;
using Shop.Persentation.Abtractions;
using static Shop.Contract.Services.V1.Suppliers.Query;

namespace Shop.Persentation.Controllers.V1;

[ApiVersion(1)]
public class SuppliersController : ApiController
{
    public SuppliersController(ISender sender) : base(sender)
    {
    }

    [Authorize]
    [HttpPost("filter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Filter([FromBody] GetSuppliersQuery request)
    {
        var result = await sender.Send(request);
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }
        ;
        return Ok(result);
    }



    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await sender.Send(new GetSupplierByIdQuery(id));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }
        ;
        return Ok(result);
    }



    [Authorize]
    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] Command.CreateSupplierCommand createCityCommand)
    {
        var result = await sender.Send(createCityCommand);
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }
        ;
        return Ok(result);
    }

    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] Command.UpdateSupplierCommand request, Guid id)
    {
        var result = await sender.Send(new Command.UpdateSupplierCommand(
            id,
                request.Code,
                request.Name,
                request.ShortName,
                request.TaxCode,
                request.Phone,
                request.Email,
                request.Website,
                request.ContactName,
                request.ContactPhone,
                request.ContactEmail,
                request.ProvinceId,
                request.WardId,
                request.AddressLine,
                request.FullAddress,
                request.BankName,
                request.BankAccountNo,
                request.BankAccountName,
                request.PaymentTermDays,
                request.Rating,
                request.Note,
                request.IsActive
            ));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }
        ;
        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await sender.Send(new Command.DeleteSupplierCommand(id));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }
        ;
        return Ok(result);
    }

//    [Authorize]
//    [HttpPost("upload")]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> Upload([FromForm] Command.UploadCustomerCommand request)
//    {
//        var result = await sender.Send(new Command.UploadCustomerCommand(request.File));
//        if (result.IsFailure)
//        {
//            return HandlerFailure(result);
//        }
//        return Ok(result);
//    }
}
