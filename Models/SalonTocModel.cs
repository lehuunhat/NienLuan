using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HienTangToc.Models
{
    public class SalonTocModel
    {
        [Key]
        public int idSalon { get; set; }

        [Required]
        public string TenSalon { get; set; }

        [Required]
        public string DiaChiSalon { get; set; }

        [Required]
        public string SDTSalon { get; set; }

        public ICollection<HTvsSalonModel> HTvsSalonModels { get; set; } = new List<HTvsSalonModel>();
        public ICollection<MTvsSalonModel> MTvsSalonModels { get; set; } = new List<MTvsSalonModel>();
    }
}
