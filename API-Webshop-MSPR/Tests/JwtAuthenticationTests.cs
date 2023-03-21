using API_Webshop_MSPR.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Claims;
using Xunit;

namespace API_Webshop_MSPR.Tests
{
    [TestClass]
    public class JwtAuthenticationTests
    {
        private JwtAuthenticationService _jwtAuthentificationService;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            _jwtAuthentificationService = new JwtAuthentificationService();
        }

        [TestMethod]
        public void Authenticate_ValidCredentials_ReturnsUser()
        {
            // Act
            Utilisateur user = _jwtAuthentificationService.Authenticate("admin@gmail.com", "Epsi2023");

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual("admin@gmail.com", user.Email);
        }

        [TestMethod]
        public void Authenticate_InvalidCredentials_ReturnsNull()
        {
            // Act
            Utilisateur user = _jwtAuthentificationService.Authenticate("invalid@gmail.com", "password");

            // Assert
            Assert.IsNull(user);
        }

        [TestMethod]
        public void GenerateToken_ValidClaims_ReturnsToken()
        {
            // Arrange
            string secret = "mysecret";
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, "admin@gmail.com"),
                new Claim(ClaimTypes.Role, "admin")
            };

            // Act
            string token = _jwtAuthentificationService.GenerateToken(secret, claims);

            // Assert
            Assert.IsNotNull(token);
            Assert.IsFalse(string.IsNullOrEmpty(token));
        }
    }
}
