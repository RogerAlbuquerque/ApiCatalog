using Microsoft.AspNetCore.Identity;

namespace ApiCatalog.Models
{
    public class ApplicationsUser: IdentityUser
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
