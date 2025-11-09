namespace Shop.Domain.Abstractions.Entities;
public interface IDateTracking
{
    DateTime CreatedAt { get; set; }
    DateTime? LastModifiedAt { get; set; }
}
