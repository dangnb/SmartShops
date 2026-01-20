using FluentValidation;
using Shop.Contract.Services.V1.Common.Products;

namespace Shop.Contract.Services.V1.Common.Products.Validators;
public class UpdateProductValidatior : AbstractValidator<Command.UpdateProductCommand>
{
    public UpdateProductValidatior()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống");
    }
}
