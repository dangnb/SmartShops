using FluentValidation;

namespace Shop.Contract.Services.V1.Customers.Validators;
public class UpdateCustomerValidatior : AbstractValidator<Command.UpdateCustomerCommand>
{
    public UpdateCustomerValidatior()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã không được để trống");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống");
    }
}
