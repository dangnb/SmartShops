using Shop.Domain.Abstractions;

namespace Shop.Domain.Entities;

public class Ward : DomainEntity<Guid>
{
    public Guid ProvincyId { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
}
