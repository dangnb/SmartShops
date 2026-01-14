using FDS.UuidV7.NetCore;
using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Abstractions;

public abstract class DomainEntity<TKey> : Entity, IEntityBase<TKey>
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

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent @event) => _domainEvents.Add(@event);
    public void ClearDomainEvents() => _domainEvents.Clear();
}

public interface IDomainEvent { }
