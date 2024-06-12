using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiCatalog.Services
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _config);    //claims is informations about user
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config); //used to extract infos from claims of expired generated token and create new token
    }
}
