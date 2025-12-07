using FluentValidation;

namespace Shop.Contract.Services.V1.Categories.Validators;
public class UpdateCategoryValidatior : AbstractValidator<Command.UpdateCategoryCommand>
{
    public UpdateCategoryValidatior()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã không được để trống");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống");
    }
}
