using DatingApp.Entity;
using DatingApp.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DatingApp.Services
{
    public class TokenService : ITokenServices
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));

        }

        public string GetToken(AppUser user)
        {
            var Claims = new List<Claim>
          {
              new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
          };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var TokenDescrobtor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.UtcNow.AddDays(20),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(TokenDescrobtor);
            return tokenHandler.WriteToken(token);

        }
    }
}
