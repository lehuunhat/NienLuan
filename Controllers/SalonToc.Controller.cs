using HienTangToc.Models;
using HienTangToc.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HienTangToc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalonTocController : ControllerBase
    {
        private readonly SalonTocService _salonTocService;

        public SalonTocController(SalonTocService salonTocService)
        {
            _salonTocService = salonTocService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSalonTocs(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var salonTocs = await _salonTocService.GetAllSalonTocsAsync(pageNumber, pageSize);
                return Ok(salonTocs);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // _logger.LogError(ex, "An error occurred while getting all salon tocs.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSalonToc([FromBody] SalonTocModel salonToc)
        {
            if (salonToc == null)
            {
                return BadRequest("Salon tóc không hợp lệ.");
            }

            try
            {
                var (createdSalonToc, message) = await _salonTocService.CreateSalonTocAsync(salonToc);
                if (createdSalonToc == null)
                {
                    return BadRequest(message);
                }
                return CreatedAtAction(nameof(GetAllSalonTocs), new { idSalon = createdSalonToc.idSalon }, createdSalonToc);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // _logger.LogError(ex, "An error occurred while creating salon toc.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{idSalon}")]
        public async Task<IActionResult> UpdateSalonToc(int idSalon, [FromBody] SalonTocModel salonTocUpdate)
        {
            if (salonTocUpdate == null || idSalon <= 0)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            try
            {
                var (originalSalonToc, updatedSalonToc, message) = await _salonTocService.UpdateSalonTocAsync(idSalon, salonTocUpdate);
                if (updatedSalonToc == null)
                {
                    return BadRequest(message);
                }
                return Ok(updatedSalonToc);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // _logger.LogError(ex, "An error occurred while updating salon toc.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{idSalon}")]
        public async Task<IActionResult> DeleteSalonToc(int idSalon)
        {
            if (idSalon <= 0)
            {
                return BadRequest("ID không hợp lệ.");
            }

            try
            {
                var response = await _salonTocService.DeleteSalonTocAsync(idSalon);
                if (!response.Success)
                {
                    return NotFound(response.Message);
                }
                return Ok(response.Message);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // _logger.LogError(ex, "An error occurred while deleting salon toc.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
