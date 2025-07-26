using FluentValidation;

namespace Shop.Contract.Services.V1.Invoices.Validators;
public class DeletePermissionValidatior : AbstractValidator<Command.DeleteInvoiceCommand>
{
    public DeletePermissionValidatior()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id quận/huyện không được để trống");
    }
}
