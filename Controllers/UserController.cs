using HienTangToc.Models;
using HienTangToc.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HienTangToc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var users = await _userService.GetAllUsersAsync(pageNumber, pageSize);
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // _logger.LogError(ex, "An error occurred while getting all users.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserModel user)
        {
            if (user == null)
            {
                return BadRequest("Người dùng không hợp lệ.");
            }

            try
            {
                var (createdUser, message) = await _userService.CreateUserAsync(user);
                if (createdUser == null)
                {
                    return BadRequest(message);
                }
                return CreatedAtAction(nameof(GetAllUsers), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // _logger.LogError(ex, "An error occurred while creating user.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserModel userUpdate)
        {
            if (userUpdate == null || id <= 0)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            try
            {
                var (originalUser, updatedUser, message) = await _userService.UpdateUserAsync(id, userUpdate);
                if (updatedUser == null)
                {
                    return BadRequest(message);
                }
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // _logger.LogError(ex, "An error occurred while updating user.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID không hợp lệ.");
            }

            try
            {
                var response = await _userService.DeleteUserAsync(id);
                if (!response.Success)
                {
                    return NotFound(response.Message);
                }
                return Ok(response.Message);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // _logger.LogError(ex, "An error occurred while deleting user.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
