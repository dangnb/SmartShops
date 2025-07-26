using FluentValidation;

namespace Shop.Contract.Services.V1.Products.Validators;
public class UpdateProductValidatior : AbstractValidator<Command.UpdateProductCommand>
{
    public UpdateProductValidatior()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã không được để trống");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống");
    }
}
