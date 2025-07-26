﻿using FluentValidation;

namespace Shop.Contract.Services.V1.Customers.Validators;
public class CreateCustomerValidatior : AbstractValidator<Command.CreateCustomerCommand>
{
    public CreateCustomerValidatior()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã Ma không được để trống");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên quận/huyện không được để trống");
    }
}
