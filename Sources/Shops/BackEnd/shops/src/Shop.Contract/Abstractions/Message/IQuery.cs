using Shop.Contract.Abstractions.Shared;
using MediatR;

namespace Shop.Contract.Abstractions.Message;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
