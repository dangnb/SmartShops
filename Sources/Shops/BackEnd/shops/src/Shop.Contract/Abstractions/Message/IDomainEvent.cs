using MediatR;

namespace Shop.Contract.Abstractions.Message;
public interface IDomainEvent : INotification
{
    int Id { get; init; }
}
