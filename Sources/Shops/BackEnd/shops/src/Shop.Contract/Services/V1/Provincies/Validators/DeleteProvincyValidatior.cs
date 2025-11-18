using FluentValidation;

namespace Shop.Contract.Services.V1.Provinces.Validators;
public class DeleteProvinceValidatior : AbstractValidator<Command.DeleteProvinceCommand>
{
    public DeleteProvinceValidatior()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id thành phố không được để trống");
    }
}
