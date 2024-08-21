using HienTangToc.Data;
using HienTangToc.Models;
using Microsoft.EntityFrameworkCore;

namespace HienTangToc.Services
{
    public class UserService
    {
        private readonly MyDbContext _context;

        public UserService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserModel>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _context.Users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<(UserModel user, string message)> CreateUserAsync(UserModel user)
        {
            if (string.IsNullOrWhiteSpace(user.Name) ||
                string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrWhiteSpace(user.Phone) ||
                string.IsNullOrWhiteSpace(user.Password) ||
                string.IsNullOrWhiteSpace(user.Role))
            {
                return (null, "Các trường thông tin không được để trống.");
            }

            var existingUser = await _context.Users
                .AnyAsync(u => u.Email == user.Email);

            if (existingUser)
            {
                return (null, "Email đã tồn tại.");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return (user, "Tạo người dùng thành công.");
        }

        public async Task<(UserModel originalUser, UserModel updatedUser, string message)> UpdateUserAsync(int id, UserModel userUpdate)
        {
            if (string.IsNullOrWhiteSpace(userUpdate.Name) ||
                string.IsNullOrWhiteSpace(userUpdate.Email) ||
                string.IsNullOrWhiteSpace(userUpdate.Phone) ||
                string.IsNullOrWhiteSpace(userUpdate.Password) ||
                string.IsNullOrWhiteSpace(userUpdate.Role))
            {
                return (null, null, "Các trường thông tin không được để trống.");
            }

            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return (null, null, "Người dùng không tồn tại.");
            }

            var userWithSameEmail = await _context.Users
                .AnyAsync(u => u.Email == userUpdate.Email && u.Id != id);

            if (userWithSameEmail)
            {
                return (null, null, "Email đã tồn tại.");
            }

            var originalUser = new UserModel
            {
                Id = existingUser.Id,
                Name = existingUser.Name,
                Email = existingUser.Email,
                Phone = existingUser.Phone,
                Password = existingUser.Password,
                Role = existingUser.Role
            };

            existingUser.Name = userUpdate.Name;
            existingUser.Email = userUpdate.Email;
            existingUser.Phone = userUpdate.Phone;
            existingUser.Password = userUpdate.Password;
            existingUser.Role = userUpdate.Role;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return (originalUser, existingUser, "Cập nhật người dùng thành công.");
        }

        public async Task<DeleteUserResponse> DeleteUserAsync(int id)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return new DeleteUserResponse
                {
                    Success = false,
                    Message = "Không tìm thấy người dùng."
                };
            }

            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();

            return new DeleteUserResponse
            {
                Success = true,
                Message = "Xóa người dùng thành công."
            };
        }

        public class DeleteUserResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
    }
}
