using FluentValidation;

namespace Shop.Contract.Services.V1.Permissions.Validators;
public class CreatePermissionValidatior : AbstractValidator<Command.CreatePermissionCommand>
{
    public CreatePermissionValidatior()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã không được để trống");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Diễn dải không được để trống");
    }
}
