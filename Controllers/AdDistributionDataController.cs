using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdDistributionDataController : ControllerBase
    {
        private readonly IRepository<AdDistributionData> _repository;

        public AdDistributionDataController(IRepository<AdDistributionData> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdDistributionData>>> GetAdDistributionData()
        {
            var adDistributionData = await _repository.GetAll();
            return Ok(adDistributionData);
        }

        [HttpGet("{songId}+{adCampaign}")]
        public async Task<ActionResult<AdDistributionData>> GetAdDistributionDataById(int songId, int adCampaign)
        {
            var adDistributionData = await _repository.GetByTwoIdentifiers(songId, adCampaign);

            if (adDistributionData == null)
            {
                return NotFound();
            }

            return Ok(adDistributionData);
        }

        [HttpPut("{songId}+{adCampaign}")]
        public async Task<IActionResult> PutAdDistributionData(int songId, 
            int adCampaign, AdDistributionData adDistributionData)
        {
            if (songId != adDistributionData.SongId || adCampaign != adDistributionData.AdCampaign)
            {
                return BadRequest();
            }

            try
            {
                await _repository.Update(adDistributionData);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!AdDistributionDataExists(id))
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
        public async Task<ActionResult<AdDistributionData>> PostAdDistributionData(
            AdDistributionData adDistributionData)
        {
            if (adDistributionData == null)
            {
                return BadRequest("Data is null.");
            }

            try
            {
                await _repository.Add(adDistributionData);
                var response = new
                {
                    Message = "Data added successfully",
                    Data = adDistributionData
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{songId}+{adCampaign}")]
        public async Task<ActionResult<AdDistributionData>> DeleteAdDistributionData(int songId, int adCampaign)
        {
            var adDistributionData = await _repository.GetByTwoIdentifiers(songId, adCampaign);
            if (adDistributionData == null)
            {
                return NotFound();
            }

            await _repository.Delete(adDistributionData);

            return Ok(adDistributionData);
        }

    }

}
