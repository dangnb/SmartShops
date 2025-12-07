using FluentValidation;

namespace Shop.Contract.Services.V1.Categories.Validators;
public class DeleteCategoryValidatior : AbstractValidator<Command.DeleteCategoryCommand>
{
    public DeleteCategoryValidatior()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id không được để trống");
    }
}
