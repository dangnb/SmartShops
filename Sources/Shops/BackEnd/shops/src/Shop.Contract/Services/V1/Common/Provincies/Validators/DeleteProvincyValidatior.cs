using FluentValidation;
using Shop.Contract.Services.V1.Common.Provincies;

namespace Shop.Contract.Services.V1.Common.Provincies.Validators;
public class DeleteProvinceValidatior : AbstractValidator<Command.DeleteProvinceCommand>
{
    public DeleteProvinceValidatior()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id thành phố không được để trống");
    }
}
