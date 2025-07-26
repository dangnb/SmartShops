using MediatR;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Users;

namespace Shop.API.Middleware;

public class JwtMiddleware : IMiddleware
{
    private readonly ISender _sender;

    public JwtMiddleware(ISender sender)
    {
        _sender = sender;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var result = await _sender.Send(new Command.ValidateTokenCommand(token!));
        if (result.IsSuccess)
        {
            // attach user to context on successful jwt validation
            context.Items["User"] = ((Result<string>)result).Value;
        }

        await next(context);
    }
}
