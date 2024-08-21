using HienTangToc.Data;
using HienTangToc.Models;
using Microsoft.EntityFrameworkCore;

namespace HienTangToc.Services
{
    public class NguoiHienService
    {
        private readonly MyDbContext _context;

        public NguoiHienService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<NguoiHienModel>> GetAllNguoiHiensAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _context.NguoiHien
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<(NguoiHienModel nguoiHien, string message)> CreateNguoiHienAsync(NguoiHienModel nguoiHien)
        {
            if (string.IsNullOrWhiteSpace(nguoiHien.HoTenNH) ||
                string.IsNullOrWhiteSpace(nguoiHien.SDTNH) ||
                string.IsNullOrWhiteSpace(nguoiHien.DiaChiNH) ||
                string.IsNullOrWhiteSpace(nguoiHien.EmailNH))
            {
                return (null, "Các trường thông tin không được để trống.");
            }

            var existingNguoiHien = await _context.NguoiHien
                .AnyAsync(nh => nh.HoTenNH == nguoiHien.HoTenNH);

            if (existingNguoiHien)
            {
                return (null, "Tên người hiến đã tồn tại.");
            }

            _context.NguoiHien.Add(nguoiHien);
            await _context.SaveChangesAsync();

            return (nguoiHien, "Tạo người hiến thành công.");
        }

        public async Task<(NguoiHienModel originalNguoiHien, NguoiHienModel updatedNguoiHien, string message)> UpdateNguoiHienAsync(int idnh, NguoiHienModel nguoiHienUpdate)
        {
            if (string.IsNullOrWhiteSpace(nguoiHienUpdate.HoTenNH) ||
                string.IsNullOrWhiteSpace(nguoiHienUpdate.SDTNH) ||
                string.IsNullOrWhiteSpace(nguoiHienUpdate.DiaChiNH) ||
                string.IsNullOrWhiteSpace(nguoiHienUpdate.EmailNH))
            {
                return (null, null, "Các trường thông tin không được để trống.");
            }

            var existingNguoiHien = await _context.NguoiHien.FindAsync(idnh);
            if (existingNguoiHien == null)
            {
                return (null, null, "Người hiến không tồn tại.");
            }

            var nguoiHienWithSameName = await _context.NguoiHien
                .AnyAsync(nh => nh.HoTenNH == nguoiHienUpdate.HoTenNH && nh.idNH != idnh);

            if (nguoiHienWithSameName)
            {
                return (null, null, "Tên người hiến đã tồn tại.");
            }

            var originalNguoiHien = new NguoiHienModel
            {
                idNH = existingNguoiHien.idNH,
                HoTenNH = existingNguoiHien.HoTenNH,
                SDTNH = existingNguoiHien.SDTNH,
                DiaChiNH = existingNguoiHien.DiaChiNH,
                EmailNH = existingNguoiHien.EmailNH
            };

            existingNguoiHien.HoTenNH = nguoiHienUpdate.HoTenNH;
            existingNguoiHien.SDTNH = nguoiHienUpdate.SDTNH;
            existingNguoiHien.DiaChiNH = nguoiHienUpdate.DiaChiNH;
            existingNguoiHien.EmailNH = nguoiHienUpdate.EmailNH;

            _context.NguoiHien.Update(existingNguoiHien);
            await _context.SaveChangesAsync();

            return (originalNguoiHien, existingNguoiHien, "Cập nhật người hiến thành công.");
        }

        public async Task<DeleteNguoiHienResponse> DeleteNguoiHienAsync(int idnh)
        {
            var existingNguoiHien = await _context.NguoiHien.FindAsync(idnh);
            if (existingNguoiHien == null)
            {
                return new DeleteNguoiHienResponse
                {
                    Success = false,
                    Message = "Không tìm thấy người hiến."
                };
            }

            _context.NguoiHien.Remove(existingNguoiHien);
            await _context.SaveChangesAsync();

            return new DeleteNguoiHienResponse
            {
                Success = true,
                Message = "Xóa người hiến thành công."
            };
        }

        public class DeleteNguoiHienResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
    }
}
