using FluentValidation;

namespace Shop.Contract.Services.V1.Districts.Validators;
public class UpdateVillageValidatior : AbstractValidator<Command.UpdateDistrictCommand>
{
    public UpdateVillageValidatior()
    {
        RuleFor(x => x.CityId).NotEmpty();
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã quận/huyện không được để trống");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên quận/huyện không được để trống");
    }
}
