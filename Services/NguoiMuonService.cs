using HienTangToc.Data;
using HienTangToc.Models;
using Microsoft.EntityFrameworkCore;

namespace HienTangToc.Services
{
    public class NguoiMuonService
    {
        private readonly MyDbContext _context;

        public NguoiMuonService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<NguoiMuonModel>> GetAllNguoiMuonsAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _context.NguoiMuon
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<(NguoiMuonModel nguoiMuon, string message)> CreateNguoiMuonAsync(NguoiMuonModel nguoiMuon)
        {
            if (string.IsNullOrWhiteSpace(nguoiMuon.HoTenNM) ||
                string.IsNullOrWhiteSpace(nguoiMuon.SDTNM) ||
                string.IsNullOrWhiteSpace(nguoiMuon.DiaChiNM) ||
                string.IsNullOrWhiteSpace(nguoiMuon.EmailNM))
            {
                return (null, "Các trường thông tin không được để trống.");
            }

            var existingNguoiMuon = await _context.NguoiMuon
                .AnyAsync(nm => nm.HoTenNM == nguoiMuon.HoTenNM);

            if (existingNguoiMuon)
            {
                return (null, "Tên người mượn đã tồn tại.");
            }

            _context.NguoiMuon.Add(nguoiMuon);
            await _context.SaveChangesAsync();

            return (nguoiMuon, "Tạo người mượn thành công.");
        }

        public async Task<(NguoiMuonModel originalNguoiMuon, NguoiMuonModel updatedNguoiMuon, string message)> UpdateNguoiMuonAsync(int idnm, NguoiMuonModel nguoiMuonUpdate)
        {
            if (string.IsNullOrWhiteSpace(nguoiMuonUpdate.HoTenNM) ||
                string.IsNullOrWhiteSpace(nguoiMuonUpdate.SDTNM) ||
                string.IsNullOrWhiteSpace(nguoiMuonUpdate.DiaChiNM) ||
                string.IsNullOrWhiteSpace(nguoiMuonUpdate.EmailNM))
            {
                return (null, null, "Các trường thông tin không được để trống.");
            }

            var existingNguoiMuon = await _context.NguoiMuon.FindAsync(idnm);
            if (existingNguoiMuon == null)
            {
                return (null, null, "Người mượn không tồn tại.");
            }

            var nguoiMuonWithSameName = await _context.NguoiMuon
                .AnyAsync(nm => nm.HoTenNM == nguoiMuonUpdate.HoTenNM && nm.idNM != idnm);

            if (nguoiMuonWithSameName)
            {
                return (null, null, "Tên người mượn đã tồn tại.");
            }

            var originalNguoiMuon = new NguoiMuonModel
            {
                idNM = existingNguoiMuon.idNM,
                HoTenNM = existingNguoiMuon.HoTenNM,
                SDTNM = existingNguoiMuon.SDTNM,
                DiaChiNM = existingNguoiMuon.DiaChiNM,
                EmailNM = existingNguoiMuon.EmailNM
            };

            existingNguoiMuon.HoTenNM = nguoiMuonUpdate.HoTenNM;
            existingNguoiMuon.SDTNM = nguoiMuonUpdate.SDTNM;
            existingNguoiMuon.DiaChiNM = nguoiMuonUpdate.DiaChiNM;
            existingNguoiMuon.EmailNM = nguoiMuonUpdate.EmailNM;

            _context.NguoiMuon.Update(existingNguoiMuon);
            await _context.SaveChangesAsync();

            return (originalNguoiMuon, existingNguoiMuon, "Cập nhật người mượn thành công.");
        }

        public async Task<DeleteNguoiMuonResponse> DeleteNguoiMuonAsync(int idnm)
        {
            var existingNguoiMuon = await _context.NguoiMuon.FindAsync(idnm);
            if (existingNguoiMuon == null)
            {
                return new DeleteNguoiMuonResponse
                {
                    Success = false,
                    Message = "Không tìm thấy người mượn."
                };
            }

            _context.NguoiMuon.Remove(existingNguoiMuon);
            await _context.SaveChangesAsync();

            return new DeleteNguoiMuonResponse
            {
                Success = true,
                Message = "Xóa người mượn thành công."
            };
        }

        public class DeleteNguoiMuonResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
    }
}
