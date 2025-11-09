using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Abstractions;
public abstract class DomainEntity<TKey>: IEntityBase<TKey>
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
}
