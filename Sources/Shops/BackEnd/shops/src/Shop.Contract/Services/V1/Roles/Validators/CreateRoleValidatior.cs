using FluentValidation;

namespace Shop.Contract.Services.V1.Roles.Validators;
public class CreateRoleValidatior : AbstractValidator<Command.CreateRoleCommand>
{
    public CreateRoleValidatior()
    {
        RuleFor(x => x.Description).NotEmpty().WithMessage("Diễn dải không được để trống");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Mã không được để trống");
    }
}
