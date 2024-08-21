using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HienTangToc.Models
{
    public class NguoiMuonModel
    {
        [Key]
        public int idNM { get; set; }

        [Required]
        public string HoTenNM { get; set; }

        [Required]
        public string SDTNM { get; set; }

        [Required]
        public string DiaChiNM { get; set; }

        [Required]
        [EmailAddress]
        public string EmailNM { get; set; }

        public ICollection<MTvsSalonModel> MTvsSalonModels { get; set; } = new List<MTvsSalonModel>();
    }
}
