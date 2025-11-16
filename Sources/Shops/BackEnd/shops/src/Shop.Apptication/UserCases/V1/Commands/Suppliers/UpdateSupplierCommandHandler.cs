using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Suppliers;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Commands.Suppliers;

public class UpdateSupplierCommandHandler : ICommandHandler<Command.UpdateSupplierCommand>
{

    private readonly IRepositoryBase<Supplier, Guid> _repositoryBase;
    private readonly ICurrentUser _currentUser;
    public UpdateSupplierCommandHandler(IRepositoryBase<Supplier, Guid> repositoryBase, ICurrentUser currentUser)
    {
        _repositoryBase= repositoryBase;
        _currentUser= currentUser;
    }
    public async Task<Result> Handle(Command.UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _repositoryBase.FindSingleAsync(x => x.Id == request.Id, cancellationToken) 
            ?? throw new KeyNotFoundException($"Supplier {request.Id} not found.");

        // Nếu cho phép đổi Code, cần check trùng
        if (!string.Equals(supplier.Code, request.Code, StringComparison.OrdinalIgnoreCase))
        {
            var exists = await _repositoryBase.AnyAsync(x => x.Code == request.Code && x.Id != request.Id, cancellationToken);

            if (exists)
                throw new InvalidOperationException($"Supplier code '{request.Code}' already exists.");

            // Nếu bạn muốn Code immutable, bỏ block này & bỏ trường Code trong command
            // Còn nếu cho đổi thì cần method ChangeCode riêng trong domain
            // supplier.ChangeCode(request.Code, _currentUser.UserId);
        }

        supplier.UpdateBasicInfo(
            name: request.Name,
            shortName: request.ShortName,
            phone: request.Phone,
            email: request.Email,
            website: request.Website,
            taxCode: request.TaxCode,
            note: request.Note
        );

        supplier.UpdateContact(
            contactName: request.ContactName,
            contactPhone: request.ContactPhone,
            contactEmail: request.ContactEmail
        );

        supplier.UpdateAddress(
            provinceId: request.ProvinceId,
            wardId: request.WardId,
            addressLine: request.AddressLine,
            fullAddress: request.FullAddress
        );

        supplier.UpdateBankInfo(
            bankName: request.BankName,
            bankAccountNo: request.BankAccountNo,
            bankAccountName: request.BankAccountName
        );

        supplier.ChangePaymentTerm(request.PaymentTermDays);

        if (request.Rating.HasValue)
        {
            supplier.Rate(request.Rating.Value);
        }

        if (request.IsActive)
            supplier.Activate();
        else
            supplier.Deactivate();

        _repositoryBase.Update(supplier);
        return Result.Success();
    }
}
