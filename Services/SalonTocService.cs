using HienTangToc.Data;
using HienTangToc.Models;
using Microsoft.EntityFrameworkCore;

namespace HienTangToc.Services
{
    public class SalonTocService
    {
        private readonly MyDbContext _context;

        public SalonTocService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<SalonTocModel>> GetAllSalonTocsAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _context.SalonToc
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<(SalonTocModel salonToc, string message)> CreateSalonTocAsync(SalonTocModel salonToc)
        {
            if (string.IsNullOrWhiteSpace(salonToc.TenSalon) ||
                string.IsNullOrWhiteSpace(salonToc.DiaChiSalon) ||
                string.IsNullOrWhiteSpace(salonToc.SDTSalon))
            {
                return (null, "Các trường thông tin không được để trống.");
            }

            var existingSalonToc = await _context.SalonToc
                .AnyAsync(st => st.TenSalon == salonToc.TenSalon);

            if (existingSalonToc)
            {
                return (null, "Tên salon đã tồn tại.");
            }

            _context.SalonToc.Add(salonToc);
            await _context.SaveChangesAsync();

            return (salonToc, "Tạo salon tóc thành công.");
        }

        public async Task<(SalonTocModel originalSalonToc, SalonTocModel updatedSalonToc, string message)> UpdateSalonTocAsync(int idSalon, SalonTocModel salonTocUpdate)
        {
            if (string.IsNullOrWhiteSpace(salonTocUpdate.TenSalon) ||
                string.IsNullOrWhiteSpace(salonTocUpdate.DiaChiSalon) ||
                string.IsNullOrWhiteSpace(salonTocUpdate.SDTSalon))
            {
                return (null, null, "Các trường thông tin không được để trống.");
            }

            var existingSalonToc = await _context.SalonToc.FindAsync(idSalon);
            if (existingSalonToc == null)
            {
                return (null, null, "Salon tóc không tồn tại.");
            }

            var salonTocWithSameName = await _context.SalonToc
                .AnyAsync(st => st.TenSalon == salonTocUpdate.TenSalon && st.idSalon != idSalon);

            if (salonTocWithSameName)
            {
                return (null, null, "Tên salon đã tồn tại.");
            }

            var originalSalonToc = new SalonTocModel
            {
                idSalon = existingSalonToc.idSalon,
                TenSalon = existingSalonToc.TenSalon,
                DiaChiSalon = existingSalonToc.DiaChiSalon,
                SDTSalon = existingSalonToc.SDTSalon
            };

            existingSalonToc.TenSalon = salonTocUpdate.TenSalon;
            existingSalonToc.DiaChiSalon = salonTocUpdate.DiaChiSalon;
            existingSalonToc.SDTSalon = salonTocUpdate.SDTSalon;

            _context.SalonToc.Update(existingSalonToc);
            await _context.SaveChangesAsync();

            return (originalSalonToc, existingSalonToc, "Cập nhật salon tóc thành công.");
        }

        public async Task<DeleteSalonTocResponse> DeleteSalonTocAsync(int idSalon)
        {
            var existingSalonToc = await _context.SalonToc.FindAsync(idSalon);
            if (existingSalonToc == null)
            {
                return new DeleteSalonTocResponse
                {
                    Success = false,
                    Message = "Không tìm thấy salon tóc."
                };
            }

            _context.SalonToc.Remove(existingSalonToc);
            await _context.SaveChangesAsync();

            return new DeleteSalonTocResponse
            {
                Success = true,
                Message = "Xóa salon tóc thành công."
            };
        }

        public class DeleteSalonTocResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
    }
}
