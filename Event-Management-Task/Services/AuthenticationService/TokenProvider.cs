using Event_Management_Task.Models;
using Event_Management_Task.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Event_Management_Task.Services.AuthenticationService
{
    public class TokenProvider
    {
        private readonly  JWT _Jwt ;
        private readonly JwtSecurityTokenHandler TokenHandler;
        public TokenProvider(IOptions<JWT> jwtOptions)
        {
            _Jwt = jwtOptions.Value;
            TokenHandler = new JwtSecurityTokenHandler();
        }

        public string Generate(User user) 
        {
            var payload = new Dictionary<string, string>
                {
                    { ClaimTypes.NameIdentifier, user.Id.ToString() },
                    { ClaimTypes.Email, user.Email },
                    { ClaimTypes.Role, user.Role }
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Jwt.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            foreach (var item in payload)
            {
                claims.Add(new Claim(item.Key, item.Value));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_Jwt.expireInMinutes),
                Issuer = _Jwt.Issuer,
                Audience = _Jwt.Audience,
                SigningCredentials = credentials
            };

            var token = TokenHandler.CreateToken(tokenDescriptor);
            return TokenHandler.WriteToken(token);
        }
    }
}
