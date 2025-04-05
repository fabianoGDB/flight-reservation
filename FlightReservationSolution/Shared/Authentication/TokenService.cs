using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shared.Authentication
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        public IEnumerable<Claim> GetClaims(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (handler.CanReadToken(token))
            {
                var jwtToken = handler.ReadJwtToken(token);
                return jwtToken?.Claims!.ToList()!;
            }
            return Enumerable.Empty<Claim>();
        }

        public bool ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var validateParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                
                handler.ValidateToken(token, validateParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }
    }
}
