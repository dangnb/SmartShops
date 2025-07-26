using FluentValidation;

namespace Shop.Contract.Services.V1.Permissions.Validators;
public class UpdateRoleValidatior : AbstractValidator<Command.UpdatePermissionCommand>
{
    public UpdateRoleValidatior()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã không được để trống");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Diễn dải không được để trống");
    }
}
