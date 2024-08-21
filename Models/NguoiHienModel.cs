using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HienTangToc.Models
{
    public class NguoiHienModel
    {
        [Key]
        public int idNH { get; set; }

        [Required]
        public string HoTenNH { get; set; }

        [Required]
        public string SDTNH { get; set; }

        [Required]
        public string DiaChiNH { get; set; }

        [Required]
        [EmailAddress]
        public string EmailNH { get; set; }

        public ICollection<HTvsSalonModel> HTvsSalonModels { get; set; } = new List<HTvsSalonModel>();
    }
}
