using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _repository;
        public UsersController(UserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string newUserUsername, string newUserPassword, string newUserCountry, string newUserEmail, int newUserAge)
        {
            if (string.IsNullOrWhiteSpace(newUserUsername))
            {
                return BadRequest("Username is required");
            }

            /*if (_repository.GetUserByUsername(newUserUsername) != null)
            {
                return BadRequest("This username is already taken");
            }*/

            if (string.IsNullOrWhiteSpace(newUserPassword))
            {
                return BadRequest("Password is required");
            }

            if (string.IsNullOrWhiteSpace(newUserCountry))
            {
                return BadRequest("Country is required");
            }

            if (string.IsNullOrWhiteSpace(newUserEmail))
            {
                return BadRequest("Email is required");
            }

            if (newUserAge < 0)
            {
                return BadRequest("Please select a valid age");
            }

            var user = new Users
            {
                UserName = newUserUsername,
                Password = newUserPassword,
                Country = newUserCountry,
                Email = newUserEmail,
                Age = newUserAge,
                Role = 1
            };

            await _repository.BcryptPassword(user);
            await _repository.Add(user);

            return Ok(new { message = "Registration successful" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string loginUsername, string loginPassword)
        {
            if (string.IsNullOrWhiteSpace(loginUsername))
            {
                return BadRequest("Username is required");
            }

            if (string.IsNullOrWhiteSpace(loginPassword))
            {
                return BadRequest("Password is required");
            }

            var user = await _repository.GetUserByUsername(loginUsername);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            if (!_repository.VerifyPassword(loginPassword, user.Password))
            {
                return BadRequest(new { message = "Incorrect password" });
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.SecretKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: "923/1",
                claims: new Claim[]
                {
                    new Claim("username", user.UserName),
                    new Claim("id", user.UserId.ToString()),
                    new Claim("role", user.Role.ToString())
                    // Add more claims as needed
                },
                signingCredentials: signinCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            // Return JWT token to client
            return Ok(new { token = tokenString });
        }
    }
}
