using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // TO BE CHANGED WHEN EMAIL TURNED TO INT
        public async Task<bool> RegisterUser(string username, string password, string country, string email, int age)
        {
            var potentialUserWithSameUsername = await _userRepository.GetUserByUsername(username);
            if (potentialUserWithSameUsername != null)
            {
                return false;
            }

            var user = new Users
            {
                UserName = username,
                Password = password,
                Country = country,
                Email = email,
                Age = age,
                Role = 1
            };

            await _userRepository.BcryptPassword(user);
            await _userRepository.Add(user);

            return true;
        }

        public async Task<string> AuthenticateUser(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);

            if (user == null)
            {
                return null;
            }

            if (!_userRepository.VerifyPassword(password, user.Password))
            {
                return null;
            }
            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(Users user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.SecretKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: "923/1",
                claims: new Claim[]
                {
                    new Claim("username", user.UserName),
                    new Claim("id", user.UserId.ToString()),
                    new Claim("role", user.Role.ToString())
                },
                signingCredentials: signinCredentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task<bool> EnableOrDisableArtist(int userId)
        {
            var user = await _userRepository.GetById(userId);

            if (user == null)
            {
                return false;
            }

            await _userRepository.EnableOrDisableArtist(user);

            return true;
        }
    }
}
