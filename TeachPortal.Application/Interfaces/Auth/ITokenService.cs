using System.Security.Claims;
using TeachersPortal.Api.Domain.Entities;
using TeachPortal.Domain.Entities;

namespace TeachersPortal.Api.Application.Interfaces.Auth
{
    public interface ITokenService
    {
        string CreateToken(AppUser user, IEnumerable<Claim>? additionalClaims = null);
        ClaimsPrincipal ValidateToken(string token);
    }
}
