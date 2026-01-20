using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Services.V1.Common.Companies;
using Shop.Domain.Entities.Identity;

namespace Shop.Apptication.UserCases.V1.Metadata.Events;
public class CreateUserWhenCompanyChangeEventHandle : IDomainEventHandler<DomainEvent.CompanyCreated>
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;
    public CreateUserWhenCompanyChangeEventHandle(IConfiguration configuration, UserManager<AppUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }
    public async Task Handle(DomainEvent.CompanyCreated notification, CancellationToken cancellationToken)
    {
        var passDefault = _configuration.GetValue<string>("PassDefault");
        var user = AppUser.CreateUser(notification.Id, notification.Username, notification.TaxCode);
        await _userManager.CreateAsync(user, passDefault);
    }
}
