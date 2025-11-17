using FluentValidation;

namespace Shop.Contract.Services.V1.Wards.Validators;
public class DeleteWardValidatior : AbstractValidator<Command.DeleteWardCommand>
{
    public DeleteWardValidatior()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id phường/xã không được để trống");
    }
}
