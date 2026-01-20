using FluentValidation;
using Shop.Contract.Services.V1.Common.Categories;

namespace Shop.Contract.Services.V1.Common.Categories.Validators;
public class CreateCategoryValidatior : AbstractValidator<Command.CreateCategoryCommand>
{
    public CreateCategoryValidatior()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã Ma không được để trống");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên quận/huyện không được để trống");
    }
}
