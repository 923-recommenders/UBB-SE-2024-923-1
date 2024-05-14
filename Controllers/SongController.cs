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
    public class SongController : ControllerBase
    {
        private readonly IRepository<Song> _repository;

        public SongController(IRepository<Song> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            var songs = await _repository.GetAll();
            return Ok(songs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSongById(int id)
        {
            var song = await _repository.GetById(id);

            if (song == null)
            {
                return NotFound();
            }

            return Ok(song);
        }

        [HttpPost]
        public async Task<ActionResult<Song>> CreateSong(Song song)
        {
            if (song == null)
            {
                return BadRequest("Song object is null");
            }

            try
            {
                await _repository.Add(song);
                var response = new
                {
                    Message = "Song created successfully",
                    Song = song
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var song = await _repository.GetById(id);
            if (song == null)
            {
                return NotFound();
            }

            await _repository.Delete(song);

            return NoContent();
        }
    }
}