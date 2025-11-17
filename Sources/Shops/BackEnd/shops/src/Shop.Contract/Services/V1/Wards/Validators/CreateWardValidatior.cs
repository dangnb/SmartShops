using FluentValidation;

namespace Shop.Contract.Services.V1.Wards.Validators;
public class CreateWardValidatior : AbstractValidator<Command.UpdateWardCommand>
{
    public CreateWardValidatior()
    {
        RuleFor(x => x.ProvincyId).NotEmpty();
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã phường/xã không được để trống");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên phường/xã không được để trống");
    }
}
