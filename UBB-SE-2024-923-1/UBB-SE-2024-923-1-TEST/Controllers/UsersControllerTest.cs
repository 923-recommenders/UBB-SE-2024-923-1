using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using UBB_SE_2024_923_1.Controllers;
using UBB_SE_2024_923_1.Services;

namespace UBB_SE_2024_923_1_TEST.Controllers
{
    public class UsersControllerTest
    {
        private readonly UsersController _controller;
        private readonly Mock<IUserService> _userServiceMock = new Mock<IUserService>();

        public UsersControllerTest()
        {
            _controller = new UsersController(_userServiceMock.Object);
        }

        [Fact]
        public async Task Register_ValidData_ReturnsOkResult()
        {
            // Arrange
            var username = "testuser";
            var password = "password";
            var country = "TestCountry";
            var email = "test@example.com";
            var age = 25;

            // Act
            var result = await _controller.Register(username, password, country, email, age) as OkObjectResult;

            //Assert
            Assert.NotNull(result.Value);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Register_ExceptionThrown_ReturnsBadRequestWithErrorMessage()
        {
            // Arrange
            var username = "testuser";
            var password = "password";
            var country = "TestCountry";
            var email = "test@example.com";
            var age = -1;

            _userServiceMock.Setup(x => x.RegisterUser(username, password, country, email, age))
                    .ThrowsAsync(new ArgumentException("Please select a valid age"));

            // Act
            var result = await _controller.Register(username, password, country, email, age) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Please select a valid age", result.Value);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var username = "validuser";
            var password = "password123";

            _userServiceMock.Setup(x => x.AuthenticateUser(username, password))
                            .ReturnsAsync("fakeJwtToken");

            // Act
            var result = await _controller.Login(username, password) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsBadRequestWithErrorMessage()
        {
            // Arrange
            var username = "invaliduser";
            var password = "wrongpassword";

            _userServiceMock.Setup(x => x.AuthenticateUser(username, password))
                            .ReturnsAsync((string)null);

            // Act
            var result = await _controller.Login(username, password) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Login_UsernameOrPasswordNotProvided_ReturnsBadRequestWithErrorMessage()
        {
            // Arrange
            string username = "";
            string password = ""; // Password not provided

            _userServiceMock.Setup(x => x.AuthenticateUser(username, password))
                            .ThrowsAsync(new ArgumentException("Username is required"));

            // Act
            var result = await _controller.Login(username, password) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Username is required", result.Value);
        }

        [Fact]
        public async Task EnableOrDisableArtist_ValidUserId_ReturnsOkResult()
        {
            // Arrange
            int userId = 1; // Assuming a valid user ID

            _userServiceMock.Setup(x => x.EnableOrDisableArtist(userId))
                            .ReturnsAsync(true);

            // Act
            var result = await _controller.EnableOrDisableArtist(userId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task EnableOrDisableArtist_InvalidUserId_ReturnsNotFoundWithErrorMessage()
        {
            // Arrange
            int userId = -1; // Assuming an invalid user ID

            _userServiceMock.Setup(x => x.EnableOrDisableArtist(userId))
                            .ThrowsAsync(new ArgumentException("Invalid user ID"));

            // Act
            var result = await _controller.EnableOrDisableArtist(userId) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}