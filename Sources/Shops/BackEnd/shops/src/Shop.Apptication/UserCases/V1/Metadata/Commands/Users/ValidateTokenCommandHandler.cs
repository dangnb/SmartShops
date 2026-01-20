using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Extensions;
using Shop.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Shop.Contract;
using Shop.Contract.Services.V1.Common.Users;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Users;

public class ValidateTokenCommandHandler : ICommandHandler<Command.ValidateTokenCommand>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;

    public ValidateTokenCommandHandler(
         UserManager<AppUser> userManager,
         IConfiguration configuration
        )
    {
        _configuration = configuration;
        _userManager = userManager;
    }
    public async Task<Result> Handle(Command.ValidateTokenCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.token))
            return Result.Failure(Error.NullValue);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]!);
        try
        {
            tokenHandler.ValidateToken(request.token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var username = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;
            return Result.Success(username);
        }
        catch
        {
            // return null if validation fails
            return Result.Failure(Error.NullValue);
        }
    }
}
