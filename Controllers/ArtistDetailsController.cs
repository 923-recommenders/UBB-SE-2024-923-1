using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_923_1.Data;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArtistDetailsController : ControllerBase
    {
        private readonly IRepository<ArtistDetails> _repository;

        public ArtistDetailsController(IRepository<ArtistDetails> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDetails>>> GetArtistDetails()
        {
            var artistDetails = await _repository.GetAll();
            return Ok(artistDetails);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDetails>> GetArtistDetailsById(int id)
        {
            var artistDetails = await _repository.GetById(id);

            if (artistDetails == null)
            {
                return NotFound();
            }

            return Ok(artistDetails);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtistDetails(int id, ArtistDetails artistDetails)
        {
            if (id != artistDetails.ArtistId)
            {
                return BadRequest();
            }

            try
            {
                await _repository.Update(artistDetails);
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ArtistDetails>> CreateArtistDetails(ArtistDetails artistDetails)
        {
            if (artistDetails == null)
            {
                return BadRequest("Artist details object is null");
            }

            try
            {
                await _repository.Add(artistDetails);
                var response = new
                {
                    Message = "Artist details created successfully",
                    ArtistDetails = artistDetails
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtistDetails(int id)
        {
            var artistDetails = await _repository.GetById(id);
            if (artistDetails == null)
            {
                return NotFound();
            }

            await _repository.Delete(artistDetails);

            return NoContent();
        }
    }
}
