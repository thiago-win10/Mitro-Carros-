using BusinessInfo.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessInfo.Application.Common.Jwt
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Domain.Entities.Issuer issuer)
        {
            var encryptionKey = Configuration.EncryptionKey;
            var key = Encoding.ASCII.GetBytes(encryptionKey);

            var claims = new[]
            {
                new Claim("IssuerId", issuer.Id.ToString()),
                new Claim("CompanyId", issuer.CompanyId.ToString()),

                new Claim(ClaimTypes.Name, issuer.Companies.ContactPerson.FullName),
                new Claim(ClaimTypes.Email, issuer.Companies.ContactPerson.Email),
                new Claim("Occupation", issuer.Companies.ContactPerson.Occupation ?? ""),
                new Claim("Phone", issuer.Companies.ContactPerson.Phone ?? "")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }


}



