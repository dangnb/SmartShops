using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Suppliers;

public class Command
{
    public record CreateSupplierCommand(
     string Code,
     string Name,
     string? ShortName,
     string? TaxCode,
     string? Phone,
     string? Email,
     string? Website,

     string? ContactName,
     string? ContactPhone,
     string? ContactEmail,

     int? ProvinceId,
     int? DistrictId,
     int? WardId,
     string? AddressLine,
     string? FullAddress,

     string? BankName,
     string? BankAccountNo,
     string? BankAccountName,

     int PaymentTermDays,
     string? Note
 ) : ICommand;
    public record UpdateSupplierCommand(
    Guid Id,

    string Code,
    string Name,
    string? ShortName,
    string? TaxCode,
    string? Phone,
    string? Email,
    string? Website,

    string? ContactName,
    string? ContactPhone,
    string? ContactEmail,

    int? ProvinceId,
    int? DistrictId,
    int? WardId,
    string? AddressLine,
    string? FullAddress,

    string? BankName,
    string? BankAccountNo,
    string? BankAccountName,

    int PaymentTermDays,
    byte? Rating,
    string? Note,
    bool IsActive
) : ICommand;
    public record DeleteProductCommand(Guid Id) : ICommand;
}
