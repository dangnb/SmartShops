using Shop.Domain.Abstractions;

namespace Shop.Domain.Entities;

public class Category : EntityAuditBase<Guid>
{
    public string Name { get; private set; } = default!;
    public string Code { get; private set; } = default!;
    public string Slug { get; private set; } = default!;
    public Guid? ParentId { get; private set; }
    public int? SortOrder { get; private set; } = default!;
    public virtual Category? Parent { get; set; }
    public virtual ICollection<Category> Children { get; set; } = new List<Category>();
    public int? Level { get; private set; }
    public bool IsActive { get; private set; } = true;
    protected Category() { }

    private Category(string name, string code, Guid? parentId, int? sortOrder, int? level, bool isActive, string slug)
    {
        Name = name;
        Code = code;
        ParentId = parentId;
        SortOrder = sortOrder;
        Level = level;
        IsActive = isActive;
        Slug = slug;

    }
    public static Category CreateEntity(string name, string code, Guid? parentId, int? sortOrder, int? level, bool isActive) => new(name, code, parentId, sortOrder, level, isActive, SlugHelper.ToSlug(name));

    public void Update(string name, Guid? parentId, int? sortOrder, int? level, bool isActive)
    {
        Name = name;
        ParentId = parentId;
        SortOrder = sortOrder;
        Level = level;
        IsActive = isActive;
        Slug = SlugHelper.ToSlug(name);
    }
}
