using FluentValidation;

namespace Shop.Contract.Services.V1.Invoices.Validators;
public class UpdatePermissionValidatior : AbstractValidator<Command.UpdateInvoiceCommand>
{
    public UpdatePermissionValidatior()
    {
        RuleFor(x => x.CityId).NotEmpty();
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã không được để trống");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống");
    }
}
