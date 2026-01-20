using FluentValidation;
using Shop.Contract.Services.V1.Common.Roles;

namespace Shop.Contract.Services.V1.Common.Roles.Validators;
public class CreateRoleValidatior : AbstractValidator<Command.CreateRoleCommand>
{
    public CreateRoleValidatior()
    {
        RuleFor(x => x.Description).NotEmpty().WithMessage("Diễn dải không được để trống");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Mã không được để trống");
    }
}
