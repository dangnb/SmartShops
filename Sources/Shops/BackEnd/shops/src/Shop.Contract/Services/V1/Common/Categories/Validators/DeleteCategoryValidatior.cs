using FluentValidation;
using Shop.Contract.Services.V1.Common.Categories;

namespace Shop.Contract.Services.V1.Common.Categories.Validators;
public class DeleteCategoryValidatior : AbstractValidator<Command.DeleteCategoryCommand>
{
    public DeleteCategoryValidatior()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id không được để trống");
    }
}
