using FluentValidation;
using Shop.Contract.Services.V1.Common.Products;

namespace Shop.Contract.Services.V1.Common.Products.Validators;
public class CreateProductValidatior : AbstractValidator<Command.CreateProductCommand>
{
    public CreateProductValidatior()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã không được để trống");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống");
    }
}
