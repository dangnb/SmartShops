using Shop.Domain.Abstractions;
using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities;
public class Category : DomainEntity<Guid>, ICompanyScopedEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public int? ParentId { get; set; }
    public Category? Parent { get; set; }
    public ICollection<Category> Children { get; set; } = new List<Category>();

    public bool IsActive { get; set; } = true;

    // Many-to-many
    //public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    public Guid ComId { get; set; }
}
