using FluentValidation;

namespace Shop.Contract.Services.V1.Provinces.Validators;
public class CreateProvinceValidatior : AbstractValidator<Command.CreateProvinceCommand>
{
    public CreateProvinceValidatior()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã thành phố không được để trống");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên thành phố không được để trống");
    }
}
