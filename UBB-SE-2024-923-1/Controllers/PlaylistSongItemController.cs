using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UBB_SE_2024_923_1.Services;

namespace UBB_SE_2024_923_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlaylistSongItemController : ControllerBase
    {
        private readonly IPlaylistSongItemService playlistSongItemService;

        public PlaylistSongItemController(IPlaylistSongItemService playlistSongItemService)
        {
            this.playlistSongItemService = playlistSongItemService ?? throw new ArgumentNullException(nameof(playlistSongItemService));
        }

        [HttpGet("SongsByPlaylist/{playlistId}")]
        public async Task<IActionResult> GetSongsByPlaylistId(int playlistId)
        {
            try
            {
                var songs = await playlistSongItemService.GetSongsByPlaylistId(playlistId);

                return Ok(songs);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("PlaylistsBySong/{songId}")]
        public async Task<IActionResult> GetPlaylistsBySongId(int songId)
        {
            try
            {
                var playlists = await playlistSongItemService.GetPlaylistsBySongId(songId);

                return Ok(playlists);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("{playlistId}/{songId}")]
        public async Task<IActionResult> AddSongToPlaylist(int playlistId, int songId)
        {
            try
            {
                await playlistSongItemService.AddSongToPlaylist(playlistId, songId);

                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("{playlistId}/{songId}")]
        public async Task<IActionResult> DeleteSongFromPlaylist(int playlistId, int songId)
        {
            try
            {
                bool deleted = await playlistSongItemService.DeleteSongFromPlaylist(playlistId, songId);

                if (!deleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
