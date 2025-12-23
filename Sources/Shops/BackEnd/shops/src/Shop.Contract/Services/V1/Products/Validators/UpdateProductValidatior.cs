using FluentValidation;

namespace Shop.Contract.Services.V1.Products.Validators;
public class UpdateProductValidatior : AbstractValidator<Command.UpdateProductCommand>
{
    public UpdateProductValidatior()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống");
    }
}
