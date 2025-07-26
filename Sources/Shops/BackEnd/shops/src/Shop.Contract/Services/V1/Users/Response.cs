namespace Shop.Contract.Services.V1.Users;
public static class Response
{
    public record UserResponse(Guid Id, string UserName, string FullName, string TaxCode, string LastName, DateTime? DayOfBirth, string Email, string PhoneNumber);
    public record UserDetailResponse(Guid Id, string UserName, string FullName, string TaxCode, string LastName, DateTime? DayOfBirth, string Email, string PhoneNumber, string[] RoleCodes);
    public record UserInforByToken(string TaxCode, string UserName, string FullName, string LastName, string FirstName);
}

