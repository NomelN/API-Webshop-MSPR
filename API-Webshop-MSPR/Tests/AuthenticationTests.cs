using API_Webshop_MSPR.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xunit;
using Moq;
using API_Webshop_MSPR.Services;

namespace API_Webshop_MSPR.Tests
{
    public class AuthenticationTests
    {
        private readonly Mock<IJwtAuthenticationService> _mockAuthService;
        private readonly IConfiguration _mockConfig;

        public AuthenticationTests()
        {
            _mockAuthService = new Mock<IJwtAuthenticationService>();
            _mockConfig = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
            {
                { "Jwt:Key", "mysecretkey" }
            }).Build();
        }

        [Fact]
        public void Login_ReturnsOk_WithValidCredentials()
        {
            // Arrange
            var controller = new AuthenticationController(_mockAuthService.Object, _mockConfig);
            var model = new ConnexionModel { Email = "admin@gmail.com", Password = "Epsi2023" };
            var claims = new List<Claim> { new Claim(ClaimTypes.Email, model.Email) };
            var token = "myjwttoken";
            _mockAuthService.Setup(s => s.Authenticate(model.Email, model.Password)).Returns(new User { Email = model.Email });
            _mockAuthService.Setup(s => s.GenerateToken(_mockConfig["Jwt:Key"], claims)).Returns(token);

            // Act
            var result = controller.Login(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualToken = Assert.IsType<string>(okResult.Value);
            Assert.Equal(token, actualToken);
        }

        [Fact]
        public void Login_ReturnsUnauthorized_WithInvalidCredentials()
        {
            // Arrange
            var controller = new AuthenticationController(_mockAuthService.Object, _mockConfig);
            var model = new ConnexionModel { Email = "invalid@gmail.com", Password = "invalidpassword" };
            _mockAuthService.Setup(s => s.Authenticate(model.Email, model.Password)).Returns((User)null);

            // Act
            var result = controller.Login(model);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
