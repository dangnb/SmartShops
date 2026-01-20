using Shop.Contract.Abstractions.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Shop.Persentation.Abtractions;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender sender;

    protected ApiController(ISender sender) => this.sender = sender;
    protected IActionResult HandlerFailure(Result result) =>
       result switch
       {
           { IsSuccess: true } => throw new InvalidOperationException(),
           IValidationResult validationResult =>
               BadRequest(
                   CreateProblemDetails(
                       "Validation Error", StatusCodes.Status400BadRequest,
                       result.Error,
                       validationResult.Errors)),
           _ =>
               BadRequest(
                   CreateProblemDetails(
                       "Bab Request", StatusCodes.Status400BadRequest,
                       result.Error))
       };

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}
