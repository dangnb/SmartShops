using FluentValidation;
using Shop.Contract.Services.V1.Common.Invoices;

namespace Shop.Contract.Services.V1.Common.Invoices.Validators;
public class DeletePermissionValidatior : AbstractValidator<Command.DeleteInvoiceCommand>
{
    public DeletePermissionValidatior()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id quận/huyện không được để trống");
    }
}
