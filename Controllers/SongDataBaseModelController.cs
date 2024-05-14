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
    public class SongDataBaseModelController : ControllerBase
    {
        private readonly IRepository<SongDataBaseModel> _repository;

        public SongDataBaseModelController(IRepository<SongDataBaseModel> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongDataBaseModel>>> GetSongDataBaseModels()
        {
            var songDataBaseModel = await _repository.GetAll();
            return Ok(songDataBaseModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SongDataBaseModel>> GetSongDataBaseModelById(int id)
        {
            var songDataBaseModel = await _repository.GetById(id);

            if (songDataBaseModel == null)
            {
                return NotFound();
            }

            return Ok(songDataBaseModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSongDataBaseModel(int id, SongDataBaseModel songDataBaseModel)
        {
            if (id != songDataBaseModel.SongId)
            {
                return BadRequest();
            }

            try
            {
                await _repository.Update(songDataBaseModel);
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<SongDataBaseModel>> PostSongDataBaseModel(SongDataBaseModel songDataBaseModel)
        {
            if (songDataBaseModel == null)
            {
                return BadRequest("Data is null.");
            }

            try
            {
                await _repository.Add(songDataBaseModel);
                var response = new
                {
                    Message = "Data added successfully",
                    Data = songDataBaseModel
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSongDataBaseModel(int id)
        {
            var songDataBaseModel = await _repository.GetById(id);
            if (songDataBaseModel == null)
            {
                return NotFound();
            }

            await _repository.Delete(songDataBaseModel);

            return Ok(songDataBaseModel);
        }
    }
}
