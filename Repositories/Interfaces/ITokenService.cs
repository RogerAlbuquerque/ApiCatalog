using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiCatalog.Repositories.Interfaces
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _config);    //claims is informations about user
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config);
    }
}
