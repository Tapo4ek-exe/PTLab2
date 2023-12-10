using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EShop.Application.Services
{
    public class JwtService
    {
        private const string Issuer = "EShopAuth";
        private const string Audience = "EShopClient";
        private const string Key = "SecretKeyForAppWithLengthMoreThan16Bytes";

        public string GetJwtTokenForUser(Guid userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userId.ToString()),
            };

            var jwt = new JwtSecurityToken(
                            issuer: Issuer,
                            audience: Audience,
                            claims: claims,
                            signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}
