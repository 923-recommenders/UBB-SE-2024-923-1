using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserDemographicsDetailsController : ControllerBase
    {
        private readonly IRepository<UserDemographicsDetails> _repository;

        public UserDemographicsDetailsController(IRepository<UserDemographicsDetails> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDemographicsDetails>>> GetUserDemographicsDetails()
        {
            var userDemographicsDetails = await _repository.GetAll();
            return Ok(userDemographicsDetails);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDemographicsDetails>> GetUserDemographicsDetailsById(int userId)
        {
            var userDemographicsDetails = await _repository.GetById(userId);

            if (userDemographicsDetails == null)
            {
                return NotFound();
            }

            return Ok(userDemographicsDetails);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> PutUserDemographicsDetails(int userId, UserDemographicsDetails userDemographicsDetails)
        {
            if (userId != userDemographicsDetails.User_Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.Update(userDemographicsDetails);
            }
            catch (DbUpdateConcurrencyException)
            {
                // return NoContent();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<UserDemographicsDetails>> PostUserDemographicsDetails(UserDemographicsDetails userDemographicsDetails)
        {
            if (userDemographicsDetails == null)
            {
                return BadRequest("Data is null.");
            }

            try
            {
                await _repository.Add(userDemographicsDetails);
                var response = new
                {
                    Message = "Data added successfully",
                    Data = userDemographicsDetails
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<UserDemographicsDetails>> DeleteUserDemographicsDetails(int userId)
        {
            var userDemographicsDetails = await _repository.GetById(userId);
            if (userDemographicsDetails == null)
            {
                return NotFound();
            }

            await _repository.Delete(userDemographicsDetails);

            return Ok(userDemographicsDetails);
        }
    }
}
