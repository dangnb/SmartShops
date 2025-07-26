using FluentValidation;

namespace Shop.Contract.Services.V1.Customers.Validators;
public class DeleteCustomerValidatior : AbstractValidator<Command.DeleteCustomerCommand>
{
    public DeleteCustomerValidatior()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id không được để trống");
    }
}
