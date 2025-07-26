using FluentValidation;

namespace Shop.Contract.Services.V1.Configs.Validators;
public class CreateConfigValidatior : AbstractValidator<Command.CreateConfigCommand>
{
    public CreateConfigValidatior()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã không được để trống");
        RuleFor(x => x.Value).NotEmpty().WithMessage("Tên không được để trống");
    }
}
