using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Valetax.App.Services;

public class TokenService
{
    public string GetToken(string code)
    {
        const string secret = "secret++=";
        const int tokenLifeTimeMinutes = 15;
        
        var expDate = new DateTimeOffset(DateTime.UtcNow.AddMinutes(tokenLifeTimeMinutes))
            .ToUnixTimeSeconds();

        var claims = new Claim[]
        {
            new(ClaimTypes.Role, "User"),
            new(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
            new(JwtRegisteredClaimNames.Exp, expDate.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var header = new JwtHeader(new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
        var payLoad = new JwtPayload(claims);
        var token = new JwtSecurityToken(header, payLoad);
        var data = new JwtSecurityTokenHandler().WriteToken(token);

        return data;
    }                
}
