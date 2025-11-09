using Shop.Domain.Abstractions;
using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities;
public class Category : DomainEntity<Guid>, ICompanyScopedEntity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public Guid ComId { get; set; }
}
