using FluentValidation;

namespace Shop.Contract.Services.V1.Cities.Validators;
public class DeleteDistrictValidatior : AbstractValidator<Command.DeleteCityCommand>
{
    public DeleteDistrictValidatior()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id thành phố không được để trống");
    }
}
