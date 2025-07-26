using Microsoft.AspNetCore.Identity;

namespace Shop.Domain.Entities.Identity;
public class AppUser : IdentityUser<Guid>
{
    public int ComId { get; set; }
    public string? Address { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string TaxCode { get; set; } = string.Empty;
    public DateTime? DayOfBirth { get; set; }
    protected AppUser() { }

    private AppUser(
        int comId,
        string userName,
        string taxCode
    )
    {
        ComId = comId;
        UserName = userName;
        TaxCode = taxCode;
    }
    private AppUser(
        int comId,
        string userName,
        string fullName,
        string passWord,
        string fistName,
        string lastName,
        string email,
        string taxCode,
        string address
        )
    {
        ComId = comId;
        UserName = userName;
        FullName = fullName;
        PasswordHash = passWord;
        FirstName = fistName;
        LastName = lastName;
        Email = email;
        TaxCode = taxCode;
        Address = address;
    }

    public static AppUser CreateUser(int comId, string userName, string taxCode)
    {
        return new AppUser(comId, userName, taxCode);
    }

    public static AppUser CreateEntity(int comId, string userName, string fullName, string fistName, string passWord, string lastName, string email, string taxCode, string address)
    {
        return new AppUser(comId, userName, fullName, passWord, fistName, lastName, email, taxCode, address);
    }

    public void Update(string userName, string fullName, string address)
    {
        UserName = userName;
        FullName = fullName;
        Address = address;
    }

    public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }
    public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; set; }
    public virtual ICollection<IdentityUserToken<Guid>> Tokens { get; set; }
    public virtual ICollection<IdentityUserRole<Guid>> Roles { get; set; }
    public virtual ICollection<AppUserDistrict> Districts { get; set; }
}
