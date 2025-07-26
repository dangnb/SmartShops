using FluentValidation;

namespace Shop.Contract.Services.V1.Districts.Validators;
public class DeleteVillageValidatior : AbstractValidator<Command.DeleteDistrictCommand>
{
    public DeleteVillageValidatior()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id quận/huyện không được để trống");
    }
}
