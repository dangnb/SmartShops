using Shop.Domain.Abstractions;

namespace Shop.Domain.Entities;

public class Provincy : DomainEntity<Guid>
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
}
