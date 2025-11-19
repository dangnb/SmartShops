using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Suppliers;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Commands.Suppliers;

public class CreateSupplierCommandHandler : ICommandHandler<Command.CreateSupplierCommand>
{
    private readonly IRepositoryBase<Supplier, Guid> _repositoryBase;

    public CreateSupplierCommandHandler(
        IRepositoryBase<Supplier, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public async Task<Result> Handle(Command.CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        // Check trùng Code (nếu cần)
        var exists = await _repositoryBase
            .AnyAsync(x => x.Code == request.Code, cancellationToken);

        if (exists)
        {
            // Bạn có thể throw custom DomainException/ValidationException tùy framework
            throw new InvalidOperationException($"Supplier code '{request.Code}' already exists.");
        }


        var supplier = Supplier.Create(
            code: request.Code,
            name: request.Name,
            shortName: request.ShortName,
            taxCode: request.TaxCode,
            phone: request.Phone,
            email: request.Email,
            website: request.Website,
            contactName: request.ContactName,
            contactPhone: request.ContactPhone,
            contactEmail: request.ContactEmail,
            provinceId: request.ProvinceId,
            wardId: request.WardId,
            addressLine: request.AddressLine,
            fullAddress: request.FullAddress,
            bankName: request.BankName,
            bankAccountNo: request.BankAccountNo,
            bankAccountName: request.BankAccountName,
            paymentTermDays: request.PaymentTermDays,
            note: request.Note
        );

        _repositoryBase.Add(supplier);
        return Result.Success();
    }
}
