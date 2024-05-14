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
    public class MostPlayedArtistInformationController : ControllerBase
    {
        private readonly IRepository<MostPlayedArtistInformation> _repository;

        public MostPlayedArtistInformationController(IRepository<MostPlayedArtistInformation> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MostPlayedArtistInformation>>> GetMostPlayedArtistInformation()
        {
            var mostPlayedArtistInformation = await _repository.GetAll();
            return Ok(mostPlayedArtistInformation);
        }

        [HttpGet("{artistId}")]
        public async Task<ActionResult<MostPlayedArtistInformation>> GetMostPlayedArtistInformationById(int artistId)
        {
            var mostPlayedArtistInformation = await _repository.GetById(artistId);

            if (mostPlayedArtistInformation == null)
            {
                return NotFound();
            }

            return Ok(mostPlayedArtistInformation);
        }

        [HttpPut("{artistId}")]
        public async Task<IActionResult> PutMostPlayedArtistInformation(int artistId, MostPlayedArtistInformation mostPlayedArtistInformation)
        {
            if (artistId != mostPlayedArtistInformation.Artist_Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.Update(mostPlayedArtistInformation);
                return Ok(mostPlayedArtistInformation);
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<MostPlayedArtistInformation>> PostMostPlayedArtistInformation(MostPlayedArtistInformation mostPlayedArtistInformation)
        {
            if (mostPlayedArtistInformation == null)
            {
                return BadRequest("Data is null.");
            }

            try
            {
                await _repository.Add(mostPlayedArtistInformation);
                var response = new
                {
                    Message = "Data added successfully",
                    Data = mostPlayedArtistInformation
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{artistId}")]
        public async Task<ActionResult<MostPlayedArtistInformation>> DeleteMostPlayedArtistInformation(int artistId)
        {
            var mostPlayedArtistInformation = await _repository.GetById(artistId);
            if (mostPlayedArtistInformation == null)
            {
                return NotFound();
            }

            await _repository.Delete(mostPlayedArtistInformation);

            return Ok(mostPlayedArtistInformation);
        }
    }
}
