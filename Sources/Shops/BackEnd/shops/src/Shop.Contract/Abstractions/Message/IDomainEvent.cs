using MediatR;

namespace Shop.Contract.Abstractions.Message;
public interface ICompanyScopedEntity : INotification
{
    Guid Id { get; init; }
}
