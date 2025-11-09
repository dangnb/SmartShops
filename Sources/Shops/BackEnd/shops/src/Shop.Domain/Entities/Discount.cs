using Shop.Domain.Abstractions;

namespace Shop.Domain.Entities;
public class Discount : EntityAuditBase<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public int Type { get; set; }
    public decimal Value { get; set; }
    public decimal MinValue { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsAntive { get; set; }
    public string Note { get; set; } = string.Empty;
}
