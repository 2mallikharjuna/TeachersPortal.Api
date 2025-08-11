using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using TeachersPortal.Api.Domain.Entities;
using Microsoft.Extensions.Configuration;
using TeachersPortal.Api.Application.Interfaces.Auth;
using TeachPortal.Domain.Entities;

namespace TeachersPortal.Api.Application.Services.Auth
{
    public class JwtTokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly byte[] _key;
        public JwtTokenService(IConfiguration config)
        {
            _config = config;
            _key = Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key"));
        }

        public string CreateToken(AppUser user, IEnumerable<Claim>? additionalClaims = null)
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpiresMinutes"] ?? "60"));
            var teacher = user.Teacher ?? throw new ArgumentException("User must be of type Teacher");
            var claims = new List<Claim>
               {
                   new Claim(ClaimTypes.NameIdentifier, teacher.Id.ToString()),
                   new Claim(ClaimTypes.Name, teacher.FirstName + " " + teacher.LastName), // Fixed CS1061 by using FirstName and LastName  
                   new Claim(ClaimTypes.Email, teacher.Email)
               };

            if (additionalClaims != null) claims.AddRange(additionalClaims);

            var creds = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer, audience, claims, expires: expires, signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _config["Jwt:Audience"],
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_key),
                ClockSkew = TimeSpan.Zero
            };

            return tokenHandler.ValidateToken(token, parameters, out _);
        }
    }
}
