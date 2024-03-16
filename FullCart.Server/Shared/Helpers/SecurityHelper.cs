using FullCart.Server.Shared.BaseModels;
using FullCart.Server.Shared.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FullCart.Server.Shared.Helpers;


public static class SecurityHelper
{
    public static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public static bool VerifyPassword(string password, string hashPassword) 
        => BCrypt.Net.BCrypt.Verify(password, hashPassword);

    public static string GenerateAccessToken(Guid userId, string username, string userRole, JWTSettings jwtSettings)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretHashKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        List<Claim> userClaims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(KeyValueConstants.UsernameClaimType, username),
            new Claim(ClaimTypes.Role, userRole),
        ];

        var token = new JwtSecurityToken
        (
            KeyValueConstants.Issuer,
            KeyValueConstants.Audience,
            userClaims.GroupBy(x => x.Value).Select(y => y.First()).Distinct(),
            DateTime.Now,
            DateTime.Now.AddMilliseconds(jwtSettings.DurationInMillisecond),
            credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
