using FluentValidation;

namespace Shop.Contract.Services.V1.Configs.Validators;
public class DeleteConfigValidatior : AbstractValidator<Command.DeleteConfigCommand>
{
    public DeleteConfigValidatior()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id không được để trống");
    }
}
