using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;
using UBB_SE_2024_923_1.Services;

namespace UBB_SE_2024_923_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password, string country, string email, int age)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Username is required");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return BadRequest("Password is required");
            }

            if (string.IsNullOrWhiteSpace(country))
            {
                return BadRequest("Country is required");
            }

            // TO BE CHANGED WHEN EMAIL TURNED TO INT
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email is required");
            }

            if (age < 0)
            {
                return BadRequest("Please select a valid age");
            }

            var isRegistered = await _userService.RegisterUser(username, password, country, email, age);

            if (!isRegistered)
            {
                return BadRequest("This username is already taken");
            }

            return Ok(new { message = "Registration successful" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Username is required");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return BadRequest("Password is required");
            }

            var token = await _userService.AuthenticateUser(username, password);

            if (token == null)
            {
                return BadRequest(new { message = "Invalid username or password" });
            }
            return Ok(new { token });
        }

        [HttpPut("{userId}/enable-disable")]
        public async Task<IActionResult> EnableOrDisableArtist(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID");
            }

            var isUpdated = await _userService.EnableOrDisableArtist(userId);

            if (!isUpdated)
            {
                return NotFound("User not found");
            }

            return Ok(new { message = "Artist status updated successfully" });
        }
    }
}

