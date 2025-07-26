using Shop.Contract.Abstractions.Shared;
using MediatR;

namespace Shop.Contract.Abstractions.Message;
public interface ICommand : IRequest<Result>
{
}
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
