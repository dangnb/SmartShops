using Asp.Versioning;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Contract.Enumerations;
using Shop.Contract.Services.V1.Categories;
using Shop.Persentation.Abtractions;
using static Shop.Contract.Services.V1.Categories.Query;

namespace Shop.Persentation.Controllers.V1;

[ApiVersion(1)]
public class CategoriesController : ApiController
{
    public CategoriesController(ISender sender) : base(sender)
    {
    }

    [Authorize]
    [HttpPost("gettree")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTree([FromBody] GetCategoriesQuery request)
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
        var result = await sender.Send(new GetCategoryByIdQuery(id));
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
    public async Task<IActionResult> Create([FromBody] Command.CreateCategoryCommand createCityCommand)
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
    public async Task<IActionResult> Update([FromBody] Command.UpdateCategoryCommand request, Guid id)
    {
        var result = await sender.Send(new Command.UpdateCategoryCommand(id, request.Code, request.Name, request.ParentId, request.SortOrder, request.Level, request.IsActive));
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
        var result = await sender.Send(new Command.DeleteCategoryCommand(id));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }
        ;
        return Ok(result);
    }

    [Authorize]
    [HttpPost("upload")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Upload([FromForm] Command.UploadCategoryCommand request)
    {
        var result = await sender.Send(new Command.UploadCategoryCommand(request.File));
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }
        return Ok(result);
    }
}
