namespace Shop.Contract;

public interface ICurrentUser
{
    Guid? UserId { get; }
    Guid? ComId { get; }            // NEW
    // Helpers tuỳ thích
    bool TryGet(string claimType, out string? value);
    Guid GetRequiredCompanyId();        // né
}
