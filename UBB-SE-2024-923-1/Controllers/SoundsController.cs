using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Services;

namespace UBB_SE_2024_923_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SoundsController : ControllerBase
    {
        private readonly ISoundService soundService;
        public SoundsController(ISoundService soundService)
        {
            this.soundService = soundService;
        }

        [HttpGet("{soundId}", Name = "GetSound")]
        public async Task<IActionResult> GetSound(int soundId)
        {
            try
            {
                var sound = await soundService.GetSoundById(soundId);
                if (sound == null)
                {
                    return NotFound();
                }

                return Ok(sound);
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

        [HttpGet]
        public async Task<IActionResult> GetAllSounds()
        {
            try
            {
                var sounds = await soundService.GetAllSounds();

                return Ok(sounds);
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

        [HttpGet("soundType/{type}")]
        public async Task<IActionResult> FilterSoundsByType(SoundType type)
        {
            try
            {
                var sounds = await soundService.FilterSoundsByType(type);

                return Ok(sounds);
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

        [HttpPost]
        public async Task<IActionResult> AddSound([FromBody] SoundForAddUpdateModel soundModel)
        {
            try
            {
                var addedSound = await soundService.AddSound(soundModel);

                return CreatedAtRoute(
                    "GetSound", new { soundId = addedSound.Id }, addedSound);
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

        [HttpDelete("{soundId}")]
        public async Task<IActionResult> DeleteSound(int soundId)
        {
            try
            {
                var deleted = await soundService.DeleteSound(soundId);
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

        [HttpPut("{soundId}")]
        public async Task<IActionResult> UpdateSound(int soundId, [FromBody] SoundForAddUpdateModel soundModel)
        {
            try
            {
                var updated = await soundService.UpdateSound(soundId, soundModel);
                if (!updated)
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
