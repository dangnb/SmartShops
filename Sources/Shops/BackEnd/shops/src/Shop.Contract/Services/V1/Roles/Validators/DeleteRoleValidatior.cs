using FluentValidation;

namespace Shop.Contract.Services.V1.Roles.Validators;
public class DeleteRoleValidatior : AbstractValidator<Command.DeleteRoleCommand>
{
    public DeleteRoleValidatior()
    {
        RuleFor(x => x.ID).NotEmpty().WithMessage("Id không được để trống");
    }
}
