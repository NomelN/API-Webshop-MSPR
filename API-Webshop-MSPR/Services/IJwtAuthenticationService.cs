using System.Security.Claims;

namespace API_Webshop_MSPR.Services
{
    public interface IJwtAuthenticationService
    {
        User Authenticate(string email, string password);
        string GenerateToken(string secret, List<Claim> claims);
    }
}
