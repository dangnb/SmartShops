using FluentValidation;

namespace Shop.Contract.Services.V1.Products.Validators;
public class CreateProductValidatior : AbstractValidator<Command.CreateProductCommand>
{
    public CreateProductValidatior()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã không được để trống");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống");
    }
}
