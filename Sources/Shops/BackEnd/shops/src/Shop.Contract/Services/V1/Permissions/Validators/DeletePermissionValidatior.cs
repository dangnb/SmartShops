using FluentValidation;

namespace Shop.Contract.Services.V1.Permissions.Validators;
public class DeletePermissionValidatior : AbstractValidator<Command.DeletePermissionCommand>
{
    public DeletePermissionValidatior()
    {
        RuleFor(x => x.ID).NotEmpty().WithMessage("Id không được để trống");
    }
}
