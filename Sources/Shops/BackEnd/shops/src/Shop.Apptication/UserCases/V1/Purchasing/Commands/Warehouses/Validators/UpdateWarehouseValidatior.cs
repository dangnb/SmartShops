using FluentValidation;
using Shop.Contract.Services.Purchasing.V1.Warehouses;

namespace Shop.Apptication.UserCases.V1.Purchasing.Commands.Warehouses.Validators;

public class UpdateWarehouseValidatior : AbstractValidator<Command.UpdateWarehouseCommand>
{
    public UpdateWarehouseValidatior()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống");
    }
}
