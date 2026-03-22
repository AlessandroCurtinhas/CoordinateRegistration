using CoordinateRegistration.Domain;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoordinateRegistration.Application.JwtAuthentication
{
    public class JwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerationToken(Person person)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            var rolesUser = new List<string>();

            foreach (var item in person.Profile)
            {
                rolesUser.Add(item.Profile.Name);

            }

            var claims = new List<Claim>
            {
                new Claim("Hash", person.Hash.ToString()),
                new Claim(ClaimTypes.Name, person.Name),
                new Claim(ClaimTypes.Email, person.Email)
            };

            claims.AddRange(rolesUser.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescritor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                  SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescritor);
            return tokenHandler.WriteToken(token);


        }
    }
}
