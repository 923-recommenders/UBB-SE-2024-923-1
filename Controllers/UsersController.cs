using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            return Ok(new { message = "Login successful" });
        }
    }
}
