using FDS.UuidV7.NetCore;
using MediatR;
using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Abstractions;

public abstract class DomainEntity<TKey> : EntityEvent, IEntityBase<TKey>
{
    public virtual TKey Id { get; set; }

    /// <summary>
    /// True if domain entity has an identity
    /// </summary>
    /// <returns></returns>
    public bool IsTransient()
    {
        return Id.Equals(default(TKey));
    }

    // Hàm này để tự động sinh UUID v7 khi cần
    public void SetIdIfEmpty()
    {
        if (Id.Equals(default(TKey)) && typeof(TKey) == typeof(Guid))
        {
            Id = (TKey)(object)UuidV7.Generate();  // UUID v7
        }
    }
}

public abstract class EntityEvent
{
    private readonly List<INotification> _domainEvents = new();

    public IReadOnlyCollection<INotification> DomainEvents
        => _domainEvents.AsReadOnly();

    protected void RaiseDomainEvent(INotification domainEvent)
        => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents()
        => _domainEvents.Clear();
}

