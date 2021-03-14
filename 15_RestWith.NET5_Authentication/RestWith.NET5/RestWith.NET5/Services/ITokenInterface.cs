using System.Collections.Generic;
using System.Security.Claims;

namespace RestWith.NET5.Services
{
    public interface ITokenInterface
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
