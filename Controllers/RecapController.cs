using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UBB_SE_2024_923_1.DTO;
using UBB_SE_2024_923_1.Enums;
using UBB_SE_2024_923_1.Services;

namespace UBB_SE_2024_923_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecapController : ControllerBase
    {
        private readonly IRecapService _recapService;

        public RecapController(IRecapService recapService)
        {
            _recapService = recapService;
        }

        [HttpGet("getTop5MostListenedSongs/{userId}")]
        public async Task<ActionResult<List<SongBasicInformation>>> GetTheTop5MostListenedSongs(int userId)
        {
            var top5MostListenedSongs = await _recapService.GetTheTop5MostListenedSongs(userId);
            return Ok(top5MostListenedSongs);
        }

        [HttpGet("getMostPlayedSongPercentile/{userId}")]
        public async Task<ActionResult<Tuple<SongBasicInformation, decimal>>> GetTheMostPlayedSongPercentile(int userId)
        {
            var mostPlayedSongPercentile = await _recapService.GetTheMostPlayedSongPercentile(userId);
            return Ok(mostPlayedSongPercentile);
        }

        [HttpGet("getMostPlayedArtistPercentile/{userId}")]
        public async Task<ActionResult<Tuple<string, decimal>>> GetTheMostPlayedArtistPercentile(int userId)
        {
            var mostPlayedArtistPercentile = await _recapService.GetTheMostPlayedArtistPercentile(userId);
            return Ok(mostPlayedArtistPercentile);
        }

        [HttpGet("getTotalMinutesListened/{userId}")]
        public async Task<ActionResult<int>> GetTotalMinutesListened(int userId)
        {
            var totalMinutesListened = await _recapService.GetTotalMinutesListened(userId);
            return Ok(totalMinutesListened);
        }

        [HttpGet("getTop5Genres/{userId}")]
        public async Task<ActionResult<List<string>>> GetTheTop5Genres(int userId)
        {
            var top5Genres = await _recapService.GetTheTop5Genres(userId);
            return Ok(top5Genres);
        }

        [HttpGet("getNewGenresDiscovered/{userId}")]
        public async Task<ActionResult<List<string>>> GetNewGenresDiscovered(int userId)
        {
            var newGenresDiscovered = await _recapService.GetNewGenresDiscovered(userId);
            return Ok(newGenresDiscovered);
        }

        [HttpGet("getListenerPersonality/{userId}")]
        public async Task<ActionResult<ListenerPersonality>> GetListenerPersonality(int userId)
        {
            var listenerPersonality = await _recapService.GetListenerPersonality(userId);
            return Ok(listenerPersonality);
        }

        [HttpGet("getEndOfYearRecap/{userId}")]
        public async Task<ActionResult<EndOfYearRecapViewModel>> GenerateEndOfYearRecap(int userId)
        {
            var endOfYearRecap = await _recapService.GenerateEndOfYearRecap(userId);
            return Ok(endOfYearRecap);
        }
    }
}
