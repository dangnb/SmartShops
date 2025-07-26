using FluentValidation;
using Shop.Contract.Services.V1.Users;

namespace Shop.Common.Services.V1.Users.Validators;
public class CreateUserValidatior : AbstractValidator<Command.CreateUserCommand>
{
    public CreateUserValidatior()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Tên đăng nhập không được để trống");
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Họ và tên không được để trống");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Định dang email không đúng");
    }
}
