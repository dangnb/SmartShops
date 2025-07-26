﻿using FluentValidation;

namespace Shop.Contract.Services.V1.Cities.Validators;
public class CreateDistrictValidatior : AbstractValidator<Command.CreateCityCommand>
{
    public CreateDistrictValidatior()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Mã thành phố không được để trống");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên thành phố không được để trống");
    }
}
