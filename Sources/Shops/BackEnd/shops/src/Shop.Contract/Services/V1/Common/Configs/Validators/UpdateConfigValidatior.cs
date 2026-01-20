using FluentValidation;
using Shop.Contract.Services.V1.Common.Configs;

namespace Shop.Contract.Services.V1.Common.Configs.Validators;
public class UpdateConfigValidatior : AbstractValidator<Command.UpdateConfigCommand>
{
    public UpdateConfigValidatior()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã không được để trống");
        RuleFor(x => x.Value).NotEmpty().WithMessage("Giá trị không được để trống");
    }
}
