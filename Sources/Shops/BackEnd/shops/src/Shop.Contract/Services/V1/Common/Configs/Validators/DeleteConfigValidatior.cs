using FluentValidation;
using Shop.Contract.Services.V1.Common.Configs;

namespace Shop.Contract.Services.V1.Common.Configs.Validators;
public class DeleteConfigValidatior : AbstractValidator<Command.DeleteConfigCommand>
{
    public DeleteConfigValidatior()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id không được để trống");
    }
}
