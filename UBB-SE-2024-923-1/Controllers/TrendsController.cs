using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrendsController : ControllerBase
    {
        private readonly IRepository<Trends> _repository;

        public TrendsController(IRepository<Trends> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trends>>> GetTrends()
        {
            var trends = await _repository.GetAll();
            return Ok(trends);
        }

        [HttpGet("{songId}+{genre}+{language}+{country}")]
        public async Task<ActionResult<Trends>> GetTrendById(int songId, string genre, string language, string country)
        {
            var trend = await _repository.GetByFourIdentifiers(songId, genre, language, country);

            if (trend == null)
            {
                return NotFound();
            }

            return Ok(trend);
        }

        [HttpPut("{songId}+{genre}+{language}+{country}")]
        public async Task<IActionResult> PutTrend(int songId, string genre, string language, string country, Trends trend)
        {
            if (songId != trend.SongId || genre != trend.Genre || language != trend.Language || country != trend.Country)
            {
                return BadRequest();
            }

            try
            {
                await _repository.Update(trend);
            }
            catch (DbUpdateConcurrencyException)
            {
                // if (!AdDistributionDataExists(id))
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
        public async Task<ActionResult<Trends>> PostTrend(Trends trend)
        {
            if (trend == null)
            {
                return BadRequest("Data is null.");
            }

            try
            {
                await _repository.Add(trend);
                var response = new
                {
                    Message = "Data added successfully",
                    Data = trend
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{songId}+{genre}+{language}+{country}")]
        public async Task<ActionResult<AdDistributionData>> DeleteTrend(int songId, string genre, string language, string country)
        {
            var trend = await _repository.GetByFourIdentifiers(songId, genre, language, country);
            if (trend == null)
            {
                return NotFound();
            }

            await _repository.Delete(trend);

            return Ok(trend);
        }
    }
}
