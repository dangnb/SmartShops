using Shop.Contract.Abstractions.Shared;
using MediatR;

namespace Shop.Contract.Abstractions.Message;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
