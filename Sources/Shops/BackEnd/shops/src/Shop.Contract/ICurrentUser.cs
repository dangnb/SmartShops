namespace Shop.Contract;

public interface ICurrentUser
{
    string? UserId { get; }
    Guid? ComId { get; }            
    // Helpers tuỳ thích
    bool TryGet(string claimType, out string? value);
    Guid GetRequiredCompanyId();       
}
