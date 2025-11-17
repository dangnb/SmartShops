using FluentValidation;

namespace Shop.Contract.Services.V1.Provincies.Validators;
public class DeleteProvincyValidatior : AbstractValidator<Command.DeleteProvincyCommand>
{
    public DeleteProvincyValidatior()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id thành phố không được để trống");
    }
}
