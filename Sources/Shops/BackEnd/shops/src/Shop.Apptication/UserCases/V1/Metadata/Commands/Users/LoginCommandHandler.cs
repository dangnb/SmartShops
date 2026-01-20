
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Extensions;
using Shop.Contract.Services.V1.Users;
using Shop.Domain.Entities.Identity;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Users;

public sealed class LoginCommandHandler : ICommandHandler<Command.LoginCommand>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IPublisher _publisher;

    public LoginCommandHandler(
        IPublisher publisher,
        UserManager<AppUser> userManager,
        IConfiguration configuration
        )
    {
        _userManager = userManager;
        _configuration = configuration;
        _publisher = publisher;
    }
    public async Task<Result> Handle(Command.LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.Where(x => x.UserName == request.UserName && x.TaxCode == request.TaxCode).FirstOrDefaultAsync();
        if (user == null)
        {
            return Result.Failure(new Error("Login false", "Thông tin đăng nhập không chính xác!"));
        }
        if (await _userManager.CheckPasswordAsync(user, request.PassWord))
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.UserName!),
                    new(CustomClaimTypes.ComId, user.ComId.ToString()),
                    new(ClaimTypes.Sid, user.TaxCode),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GetToken(authClaims);
            return Result.Success(new
            {
                authToken = new JwtSecurityTokenHandler().WriteToken(token),
                expiresIn = token.ValidTo,
                refreshToken = ""
            });
        }
        else
        {
            return Result.Failure(new Error("LOGIN_FALSE", "Login false!"));
        }

    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(int.Parse(_configuration["JWT:NumberMinuteExpired"]!)),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }
}
