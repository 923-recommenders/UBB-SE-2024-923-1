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
            try
            {
                var isRegistered = await _userService.RegisterUser(username, password, country, email, age);

                return Ok(new { message = "Registration successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                var token = await _userService.AuthenticateUser(username, password);

                if (token == null)
                {
                    return BadRequest(new { message = "Invalid username or password" });
                }

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{userId}/enable-disable")]
        public async Task<IActionResult> EnableOrDisableArtist(int userId)
        {
            try
            {
                var isUpdated = await _userService.EnableOrDisableArtist(userId);

                if (!isUpdated)
                {
                    return NotFound("User not found");
                }

                return Ok(new { message = "Artist status updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}