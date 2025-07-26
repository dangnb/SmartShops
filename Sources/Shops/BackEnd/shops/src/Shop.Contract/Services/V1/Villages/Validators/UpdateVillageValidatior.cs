using FluentValidation;

namespace Shop.Contract.Services.V1.Villages.Validators;
public class UpdateVillageValidatior : AbstractValidator<Command.UpdateVillageCommand>
{
    public UpdateVillageValidatior()
    {
        RuleFor(x => x.WardId).NotEmpty();
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã không được để trống");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống");
    }
}
