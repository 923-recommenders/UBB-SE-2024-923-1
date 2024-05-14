using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_923_1.Data;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1_TEST.Repositories
{
    public class UserRepositoryTests
    {
        private DataContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            return new DataContext(options);
        }

        [Fact]
        public async Task BcryptPassword_ShouldEncryptPassword_PasswordSuccesfulyEncrypted()
        {
            // Arrange
            var dbContext = CreateDbContext();
            var userRepository = new UserRepository(dbContext);
            var user = new Users
            {
                UserName = "testuser",
                Password = "password"
            };

            // Act
            await userRepository.BcryptPassword(user);

            // Assert
            Assert.NotNull(user.Password);
            Assert.NotEqual("password", user.Password); // Ensure password is encrypted
        }

        [Fact]
        public async Task VerifyPassword_ShouldReturnTrue_WhenPasswordIsCorrect()
        {
            // Arrange
            var userRepository = new UserRepository(CreateDbContext());
            string password = "password";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password + "a$^#shfdyu$^%agb@#%jqd#!cbjhacs!@#!b");

            // Act
            bool result = userRepository.VerifyPassword(password, hashedPassword);

            // Assert
            Assert.True(result);
        }


        [Fact]
        public async Task VerifyPassword_ShouldReturnFalse_WhenPasswordIsIncorrect()
        {
            // Arrange
            var userRepository = new UserRepository(CreateDbContext());
            string password = "wrong_password";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword("correct_password" + "a$^#shfdyu$^%agb@#%jqd#!cbjhacs!@#!b");

            // Act
            bool result = userRepository.VerifyPassword(password, hashedPassword);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task EnableOrDisableArtist_ShouldToggleUserRoleFrom1To2_UserIsNowInRole2()
        {
            // Arrange
            var dbContext = CreateDbContext();
            var userRepository = new UserRepository(dbContext);
            var user = new Users
            {
                UserName = "testuser",
                Password = "password",
                Country = "Romania",
                Email = "email@email.com",
                Role = 1 // Assuming Role 1 is for an artist
            };
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            // Act
            await userRepository.EnableOrDisableArtist(user);

            // Assert
            var updatedUser = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
            Assert.NotNull(updatedUser);
            Assert.Equal(2, updatedUser.Role); // Role should be toggled from 1 to 2
        }

        [Fact]
        public async Task EnableOrDisableArtist_ShouldToggleUserRoleFrom2To1_UserIsNowInRole1()
        {
            // Arrange
            var dbContext = CreateDbContext();
            var userRepository = new UserRepository(dbContext);
            var user = new Users
            {
                UserName = "testuser",
                Password = "password",
                Country = "Romania",
                Email = "email@email.com",
                Role = 2 // Assuming Role 2 is not for an artist
            };
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            // Act
            await userRepository.EnableOrDisableArtist(user);

            // Assert
            var updatedUser = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
            Assert.NotNull(updatedUser);
            Assert.Equal(1, updatedUser.Role); // Role should be toggled from 2 to 1
        }

        [Fact]
        public async Task GetUserByUsername_WhenUserExists_ShouldReturnUser()
        {
            // Arrange
            var dbContext = CreateDbContext();
            var userRepository = new UserRepository(dbContext);
            var user = new Users
            {
                UserName = "testuser",
                Password = "password",
                Country = "Romania",
                Email = "email@email.com",
                Role = 1
            };
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await userRepository.GetUserByUsername("testuser");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.UserName, result.UserName);
        }

        [Fact]
        public async Task GetUserByUsername_WhenUserDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var dbContext = CreateDbContext();
            var userRepository = new UserRepository(dbContext);

            // Act
            var result = await userRepository.GetUserByUsername("nonexistentuser");

            // Assert
            Assert.Null(result);
        }
    }
}
