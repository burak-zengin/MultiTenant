using Microsoft.IdentityModel.Tokens;
using Identity.Domain.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Infrustructure.Security;

public class JwtGenerator
{
    public string Generate(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var privateKey = Encoding.UTF8.GetBytes("bAafd@A7d9#@F4*V!LHZs#ebKQrkE6pad2f3kj34c3dXy@");

        var credentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256);

        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim("id", user.Id.ToString()));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(1),
            Subject = claimsIdentity
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }
}
