using FluentValidation;
using Shop.Contract.Services.V1.Common.Companies;

namespace Shop.Contract.Services.V1.Common.Companies.Validators;
public class CreateCompanyValidatior : AbstractValidator<Command.CreateCompanyCommand>
{
    public CreateCompanyValidatior()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã đơn vị không được để trống");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên đơn vị không được để trống");
    }
}
