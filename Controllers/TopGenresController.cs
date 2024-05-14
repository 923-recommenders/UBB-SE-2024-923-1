using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UBB_SE_2024_923_1.Services;
using UBB_SE_2024_923_1.DTO;

namespace UBB_SE_2024_923_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TopGenresController : ControllerBase
    {
        private readonly TopGenresService _topGenresService;

        public TopGenresController(TopGenresService topGenresService)
        {
            _topGenresService = topGenresService;
        }

        [HttpGet("top-genres/{month}/{year}")]
        public async Task<ActionResult<IEnumerable<GenreData>>> GetTopGenres(int month, int year)
        {
            var topGenres = await _topGenresService.GetTop3Genres(month, year);
            return Ok(topGenres);
        }

        [HttpGet("top-sub-genres/{month}/{year}")]
        public async Task<ActionResult<IEnumerable<GenreData>>> GetTopSubGenres(int month, int year)
        {
            var topSubGenres = await _topGenresService.GetTop3SubGenres(month, year);
            return Ok(topSubGenres);
        }
    }
}
