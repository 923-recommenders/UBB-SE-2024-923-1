using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SongRecommendationsDetailsController : ControllerBase
    {
        private readonly IRepository<SongRecommendationDetails> _repository;

        public SongRecommendationsDetailsController(IRepository<SongRecommendationDetails> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongRecommendationDetails>>> GetSongRecommendationDetails()
        {
            var songRecommendationDetails = await _repository.GetAll();
            return Ok(songRecommendationDetails);
        }

        [HttpGet("{songId}+{month}+{year}")]
        public async Task<ActionResult<SongRecommendationDetails>> GetSongRecommendationDetailsById(int songId,
            int month, int year)
        {
            var songRecommendationDetails = await _repository.GetByThreeIdentifiers(songId, month, year);

            if (songRecommendationDetails == null)
            {
                return NotFound();
            }

            return Ok(songRecommendationDetails);
        }

        [HttpPut("{songId}+{month}+{year}")]
        public async Task<IActionResult> PutSongRecommendationDetails(int songId, int month, int year,
            SongRecommendationDetails songRecommendationDetails)
        {
            try
            {
                await _repository.Update(songRecommendationDetails);
            }
            catch (DbUpdateConcurrencyException)
            {
                // if (!SongRecommendationDetailsExists(id))
                // {
                //    return NotFound();
                // }
                // else
                // {
                //    throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<SongRecommendationDetails>> PostSongRecommendationDetails(
            SongRecommendationDetails songRecommendationDetails)
        {
            await _repository.Add(songRecommendationDetails);

            return CreatedAtAction("GetSongRecommendationDetails", new { id = songRecommendationDetails.SongId }, songRecommendationDetails);
        }

        [HttpDelete("{songId}+{month}+{year}")]
        public async Task<IActionResult> DeleteSongRecommendationDetails(int songId, int month, int year)
        {
            var songRecommendationDetails = await _repository.GetByThreeIdentifiers(songId, month, year);
            if (songRecommendationDetails == null)
            {
                return NotFound();
            }

            await _repository.Delete(songRecommendationDetails);

            return NoContent();
        }
    }
}
