using FluentValidation;
using Shop.Contract.Services.V1.Common.Wards;

namespace Shop.Contract.Services.V1.Common.Wards.Validators;
public class CreateWardValidatior : AbstractValidator<Command.UpdateWardCommand>
{
    public CreateWardValidatior()
    {
        RuleFor(x => x.ProvinceId).NotEmpty();
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã phường/xã không được để trống");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên phường/xã không được để trống");
    }
}
