using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserPlaybackBehaviourController : ControllerBase
    {
        private readonly IRepository<UserPlaybackBehaviour> _repository;

        public UserPlaybackBehaviourController(IRepository<UserPlaybackBehaviour> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPlaybackBehaviour>>> GetUserPlaybackBehaviour()
        {
            var userPlaybackBehaviour = await _repository.GetAll();
            return Ok(userPlaybackBehaviour);
        }

        [HttpGet("{userId}+{songId}+{timestamp}")]
        public async Task<ActionResult<UserPlaybackBehaviour>> GetUserPlaybackBehaviourById(int userId, int songId,
            DateTime timestamp)
        {
            var userPlaybackBehaviour = await _repository.GetByThreeIdentifiers(userId, songId, timestamp);

            if (userPlaybackBehaviour == null)
            {
                return NotFound();
            }

            return Ok(userPlaybackBehaviour);
        }

        [HttpPut("{userId}+{songId}+{timestamp}")]
        public async Task<IActionResult> PutUserPlaybackBehaviour(int userId, int songId, DateTime timestamp,
            UserPlaybackBehaviour userPlaybackBehaviour)
        {
            if (userId != userPlaybackBehaviour.UserId || songId != userPlaybackBehaviour.SongId ||
                timestamp != userPlaybackBehaviour.Timestamp)
            {
                return BadRequest();
            }

            try
            {
                await _repository.Update(userPlaybackBehaviour);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!UserPlaybackBehaviourExists(userId, songId, timestamp))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<UserPlaybackBehaviour>> CreateUserPlaybackBehaviour(
            UserPlaybackBehaviour userPlaybackBehaviour)
        {
            if (userPlaybackBehaviour == null)
            {
                return BadRequest();
            }

            await _repository.Add(userPlaybackBehaviour);

            return CreatedAtAction("GetUserPlaybackBehaviour", new { userId = userPlaybackBehaviour.UserId, songId = userPlaybackBehaviour.SongId, timestamp = userPlaybackBehaviour.Timestamp }, userPlaybackBehaviour);
        }

        [HttpDelete("{userId}+{songId}+{timestamp}")]
        public async Task<IActionResult> DeleteUserPlaybackBehaviour(int userId, int songId, DateTime timestamp)
        {
            var userPlaybackBehaviour = await _repository.GetByThreeIdentifiers(userId, songId, timestamp);
            if (userPlaybackBehaviour == null)
            {
                return NotFound();
            }

            await _repository.Delete(userPlaybackBehaviour);

            return NoContent();
        }
    }

}
