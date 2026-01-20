using FluentValidation;
using Shop.Contract.Services.V1.Common.Products;

namespace Shop.Contract.Services.V1.Common.Products.Validators;
public class DeleteProductValidatior : AbstractValidator<Command.DeleteProductCommand>
{
    public DeleteProductValidatior()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id không được để trống");
    }
}
