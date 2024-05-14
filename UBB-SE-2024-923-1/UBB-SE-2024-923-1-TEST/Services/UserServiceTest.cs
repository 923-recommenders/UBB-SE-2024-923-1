using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_923_1.Repositories;
using UBB_SE_2024_923_1.Services;
using UBB_SE_2024_923_1_TEST.Services.Stubs;
using UBB_SE_2024_923_1.Models;
using NuGet.Common;

namespace UBB_SE_2024_923_1_TEST.Services
{
    public class UserServiceTest
    {
        [Fact]
        public async Task RegisterUser_WithValidInput_ShouldReturnTrue()
        {
            // Arrange
            var userService = new UserService(new TestUserRepository());

            // Act
            var result = await userService.RegisterUser("testuser", "password123", "Country", "test@example.com", 25);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RegisterUser_WithEmptyUsername_ShouldThrowArgumentException()
        {
            // Arrange
            var userService = new UserService(new TestUserRepository());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await userService.RegisterUser("", "password123", "Country", "test@example.com", 25);
            });
        }

        [Fact]
        public async Task RegisterUser_WithEmptyPassword_ShouldThrowArgumentException()
        {
            // Arrange
            var userService = new UserService(new TestUserRepository());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await userService.RegisterUser("testuser", "", "Country", "test@example.com", 25);
            });
        }

        [Fact]
        public async Task RegisterUser_WithEmptyCountry_ShouldThrowArgumentException()
        {
            // Arrange
            var userService = new UserService(new TestUserRepository());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await userService.RegisterUser("testuser", "password123", "", "test@example.com", 24);
            });
        }

        [Fact]
        public async Task RegisterUser_WithEmptyEmail_ShouldThrowArgumentException()
        {
            // Arrange
            var userService = new UserService(new TestUserRepository());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await userService.RegisterUser("testuser", "password123", "Country", "", 25);
            });
        }

        [Fact]
        public async Task RegisterUser_WithNegativeAge_ShouldThrowArgumentException()
        {
            // Arrange
            var userService = new UserService(new TestUserRepository());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await userService.RegisterUser("testuser", "password123", "Country", "test@example.com", -25);
            });
        }

        [Fact]
        public async Task RegisterUser_WithExistingUsername_ShouldThrowArgumentException()
        {
            // Arrange
            var userRepository = new TestUserRepository();
            await userRepository.Add(new Users { UserName = "existinguser", Password = "password123", Country = "Country", Email = "test@example.com", Age = 25 });

            var userService = new UserService(userRepository);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await userService.RegisterUser("existinguser", "password123", "Country", "test@example.com", 25);
            });
        }

        [Fact]
        public async Task AuthenticateUser_WithEmptyUsername_ShouldThrowArgumentException()
        {
            // Arrange
            var userService = new UserService(new TestUserRepository());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await userService.AuthenticateUser("", "password123");
            });
        }

        [Fact]
        public async Task AuthenticateUser_WithEmptyPassword_ShouldThrowArgumentException()
        {
            // Arrange
            var userService = new UserService(new TestUserRepository());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await userService.AuthenticateUser("testuser", "");
            });
        }

        [Fact]
        public async Task AuthenticateUser_WithInvalidUsername_ShouldThrowArgumentException()
        {
            // Arrange
            var userRepository = new TestUserRepository();
            await userRepository.Add(new Users { UserName = "existinguser", Password = "password123", Country = "Country", Email = "test@example.com", Age = 25 });

            var userService = new UserService(userRepository);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await userService.AuthenticateUser("nonexistentuser", "password123");
            });
        }

        [Fact]
        public async Task AuthenticateUser_WithInvalidPassword_ShouldReturnNull()
        {
            // Arrange
            var userRepository = new TestUserRepository();
            await userRepository.Add(new Users { UserName = "testuser", Password = "password123", Country = "Country", Email = "test@example.com", Age = 25 });

            var userService = new UserService(userRepository);

            // Act
            var token = await userService.AuthenticateUser("testuser", "invalidpassword");

            // Assert
            Assert.Null(token);
        }

        [Fact]
        public async Task AuthenticateUser_WithValidCredentials_ShouldReturnJwtToken()
        {
            // Arrange
            var userRepository = new TestUserRepository();
            await userRepository.Add(new Users { UserName = "testuser", Password = "password123", Country = "Country", Email = "test@example.com", Age = 25 });

            var userService = new UserService(userRepository);

            // Act
            var token = await userService.AuthenticateUser("testuser", "password123");

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);

            var generatedToken = userService.GenerateJwtToken(await userRepository.GetUserByUsername("testuser"));
            Assert.Equal(token, generatedToken);
        }

        [Fact]
        public async Task EnableOrDisableArtist_WithValidUserId_ShouldReturnTrue()
        {
            // Arrange
            var userService = new UserService(new TestUserRepository());
            await userService.RegisterUser("testuser", "password123", "Country", "test@example.com", 25);
            // Act
            var result = await userService.EnableOrDisableArtist(1);
        }

        [Fact]
        public async Task EnableOrDisableArtist_WithInvalidUserId_ShouldThrowArgumentException()
        {
            // Arrange
            var userService = new UserService(new TestUserRepository());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await userService.EnableOrDisableArtist(-1);
            });
        }

        [Fact]
        public async Task EnableOrDisableArtist_WithNonexistentUserId_ShouldThrowArgumentException()
        {
            // Arrange
            var userRepository = new TestUserRepository();
            var userService = new UserService(userRepository);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await userService.EnableOrDisableArtist(123);
            });
        }
    }
}