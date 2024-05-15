using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UBB_SE_2024_923_1.Services;
using UBB_SE_2024_923_1.DTO;

namespace UBB_SE_2024_923_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FullDetailsOnSongController : ControllerBase
    {
        private readonly IFullDetailsOnSongService fullDetailsOnSongService;

        public FullDetailsOnSongController(IFullDetailsOnSongService fullDetailsService)
        {
            fullDetailsOnSongService = fullDetailsService;
        }

        [HttpGet("GetFullDetailsOnSong/{songId}")]
        public async Task<ActionResult<FullDetailsOnSong>> GetFullDetailsOnSong(int songId)
        {
            var fullDetailsOnSong = await fullDetailsOnSongService.GetFullDetailsOnSong(songId);
            return Ok(fullDetailsOnSong);
        }

        [HttpGet("GetCurrentMonthDetails/{songId}")]
        public async Task<ActionResult<FullDetailsOnSong>> GetCurrentMonthDetails(int songId)
        {
            var currentMonthDetails = await fullDetailsOnSongService.GetCurrentMonthDetails(songId);
            return Ok(currentMonthDetails);
        }
    }
}
