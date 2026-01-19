using MediatR;
using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Abstractions;
public class EntityAuditBase<TKey> : DomainEntity<TKey>, IEntityAuditBase<TKey>, ICompanyScopedEntity
{
    public Guid ComId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? LastModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    private readonly List<INotification> _domainEvents = new();
    public new IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    protected void Raise(INotification ev) => _domainEvents.Add(ev);
    public new void ClearDomainEvents() => _domainEvents.Clear();
}
