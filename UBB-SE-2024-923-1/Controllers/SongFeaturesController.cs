using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SongFeaturesController : ControllerBase
    {
        private readonly IRepository<SongFeatures> _repository;

        public SongFeaturesController(IRepository<SongFeatures> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongFeatures>>> GetSongFeatures()
        {
            var songFeatures = await _repository.GetAll();
            return Ok(songFeatures);
        }

        [HttpGet("{songId}+{artistId}")]
        public async Task<ActionResult<SongFeatures>> GetSongFeaturesById(int songId, int artistId)
        {
            var songFeatures = await _repository.GetByTwoIdentifiers(songId, artistId);

            if (songFeatures == null)
            {
                return NotFound();
            }

            return Ok(songFeatures);
        }

        [HttpPut("{songId}+{artistId}")]
        public async Task<IActionResult> PutSongFeatures(int songId, int artistId, SongFeatures songFeatures)
        {
            if (songId != songFeatures.SongId || artistId != songFeatures.ArtistId)
            {
                return BadRequest();
            }

            try
            {
                await _repository.Update(songFeatures);
            }
            catch (DbUpdateConcurrencyException)
            {
                // if (!SongFeaturesExists(id))
                // {
                //    return NotFound();
                // }
                // else
                // {
                //    throw;
                // }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<SongFeatures>> PostSongFeatures(SongFeatures songFeatures)
        {
            if (songFeatures == null)
            {
                return BadRequest("Data is null.");
            }

            try
            {
                await _repository.Add(songFeatures);
                var response = new
                {
                    Message = "Data added successfully",
                    Data = songFeatures
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{songId}+{artistId}")]
        public async Task<ActionResult<SongFeatures>> DeleteSongFeatures(int songId, int artistId)
        {
            var songFeatures = await _repository.GetByTwoIdentifiers(songId, artistId);
            if (songFeatures == null)
            {
                return NotFound();
            }

            await _repository.Delete(songFeatures);

            return Ok(songFeatures);
        }
    }
}
