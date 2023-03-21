using API_Webshop_MSPR.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_Webshop_MSPR.Controllers
{
    [Route("api/webshop/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtAuthenticationService _jwtAuthenticationService;
        private IConfiguration _config;
        public AuthenticationController(IJwtAuthenticationService jwtAuthentificationService, IConfiguration config)
        {
            _jwtAuthenticationService = jwtAuthentificationService;
            _config = config;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] ConnexionModel model)
        {
            var utilisateur = _jwtAuthenticationService.Authenticate(model.Email, model.Password);
            if (utilisateur != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, utilisateur.Email),
                };
                var token = _jwtAuthenticationService.GenerateToken(_config["Jwt:Key"], claims);
                return Ok(token);
            }
            return Unauthorized();

        }

    }
}
