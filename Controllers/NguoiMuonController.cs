using HienTangToc.Models;
using HienTangToc.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HienTangToc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiMuonController : ControllerBase
    {
        private readonly NguoiMuonService _nguoiMuonService;

        public NguoiMuonController(NguoiMuonService nguoiMuonService)
        {
            _nguoiMuonService = nguoiMuonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNguoiMuons(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var nguoiMuons = await _nguoiMuonService.GetAllNguoiMuonsAsync(pageNumber, pageSize);
                return Ok(nguoiMuons);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // _logger.LogError(ex, "An error occurred while getting all nguoi muons.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNguoiMuon([FromBody] NguoiMuonModel nguoiMuon)
        {
            if (nguoiMuon == null)
            {
                return BadRequest("Người mượn không hợp lệ.");
            }

            try
            {
                var (createdNguoiMuon, message) = await _nguoiMuonService.CreateNguoiMuonAsync(nguoiMuon);
                if (createdNguoiMuon == null)
                {
                    return BadRequest(message);
                }
                return CreatedAtAction(nameof(GetAllNguoiMuons), new { idnm = createdNguoiMuon.idNM }, createdNguoiMuon);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // _logger.LogError(ex, "An error occurred while creating nguoi muon.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{idnm}")]
        public async Task<IActionResult> UpdateNguoiMuon(int idnm, [FromBody] NguoiMuonModel nguoiMuonUpdate)
        {
            if (nguoiMuonUpdate == null || idnm <= 0)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            try
            {
                var (originalNguoiMuon, updatedNguoiMuon, message) = await _nguoiMuonService.UpdateNguoiMuonAsync(idnm, nguoiMuonUpdate);
                if (updatedNguoiMuon == null)
                {
                    return BadRequest(message);
                }
                return Ok(updatedNguoiMuon);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // _logger.LogError(ex, "An error occurred while updating nguoi muon.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{idnm}")]
        public async Task<IActionResult> DeleteNguoiMuon(int idnm)
        {
            if (idnm <= 0)
            {
                return BadRequest("ID không hợp lệ.");
            }

            try
            {
                var response = await _nguoiMuonService.DeleteNguoiMuonAsync(idnm);
                if (!response.Success)
                {
                    return NotFound(response.Message);
                }
                return Ok(response.Message);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // _logger.LogError(ex, "An error occurred while deleting nguoi muon.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
