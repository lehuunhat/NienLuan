using HienTangToc.Models;
using HienTangToc.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HienTangToc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiHienController : ControllerBase
    {
        private readonly NguoiHienService _nguoiHienService;

        public NguoiHienController(NguoiHienService nguoiHienService)
        {
            _nguoiHienService = nguoiHienService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNguoiHiens(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var nguoiHiens = await _nguoiHienService.GetAllNguoiHiensAsync(pageNumber, pageSize);
                return Ok(nguoiHiens);
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNguoiHien([FromBody] NguoiHienModel nguoiHien)
        {
            if (nguoiHien == null)
            {
                return BadRequest("Người hiến không hợp lệ.");
            }

            try
            {
                var (createdNguoiHien, message) = await _nguoiHienService.CreateNguoiHienAsync(nguoiHien);
                if (createdNguoiHien == null)
                {
                    return BadRequest(message);
                }
                return CreatedAtAction(nameof(GetAllNguoiHiens), new { idnh = createdNguoiHien.idNH }, createdNguoiHien);
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{idnh}")]
        public async Task<IActionResult> UpdateNguoiHien(int idnh, [FromBody] NguoiHienModel nguoiHienUpdate)
        {
            if (nguoiHienUpdate == null || idnh <= 0)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            try
            {
                var (originalNguoiHien, updatedNguoiHien, message) = await _nguoiHienService.UpdateNguoiHienAsync(idnh, nguoiHienUpdate);
                if (updatedNguoiHien == null)
                {
                    return BadRequest(message);
                }
                return Ok(updatedNguoiHien);
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{idnh}")]
        public async Task<IActionResult> DeleteNguoiHien(int idnh)
        {
            if (idnh <= 0)
            {
                return BadRequest("ID không hợp lệ.");
            }

            try
            {
                var response = await _nguoiHienService.DeleteNguoiHienAsync(idnh);
                if (!response.Success)
                {
                    return NotFound(response.Message);
                }
                return Ok(response.Message);
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
