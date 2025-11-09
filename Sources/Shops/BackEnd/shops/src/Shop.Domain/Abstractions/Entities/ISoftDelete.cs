namespace Shop.Domain.Abstractions.Entities;
public interface ISoftDelete
{
    bool IsDeleted { get; set; }
    DateTime? DeletedAt { get; set; }
    void Undo()
    {
        IsDeleted = false;
        DeletedAt = null;
    }
}
