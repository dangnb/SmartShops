using FluentValidation;
using Shop.Contract.Services.V1.Purchasing.Warehouses;

namespace Shop.Apptication.UserCases.V1.Purchasing.Commands.Warehouses.Validators;

public class CreateWarehouseValidatior : AbstractValidator<Command.CreateWarehouseCommand>
{
    public CreateWarehouseValidatior()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã Ma không được để trống");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên quận/huyện không được để trống");
    }
}
