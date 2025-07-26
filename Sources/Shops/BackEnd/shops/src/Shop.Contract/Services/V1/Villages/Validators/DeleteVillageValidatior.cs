using FluentValidation;

namespace Shop.Contract.Services.V1.Villages.Validators;
public class DeleteVillageValidatior : AbstractValidator<Command.DeleteVillageCommand>
{
    public DeleteVillageValidatior()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id không được để trống");
    }
}
