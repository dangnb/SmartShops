using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Contract.Services.V1.Common.Companies;
using Shop.Persentation.Abtractions;

namespace Shop.Persentation.Controllers.V1;
[ApiVersion(1)]
public class CompaniesController : ApiController
{
    public CompaniesController(ISender sender) : base(sender)
    {
    }

    //[Authorize]
    [HttpPost(Name = "CreateCompany")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateCompany([FromBody] Command.CreateCompanyCommand createCompanyCommand)
    {
        var result = await sender.Send(createCompanyCommand);
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }

}
