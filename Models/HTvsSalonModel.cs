using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HienTangToc.Models
{
    public class HTvsSalonModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HTvsSalonId { get; set; }
        [Required]
        public int idNH { get; set; }
        [Required]
        public int idSalon { get; set; }

        [ForeignKey("idNH")]
        public NguoiHienModel NguoiHienModel { get; set; }

        [ForeignKey("idSalon")]
        public SalonTocModel SalonTocModel { get; set; }
    }
}
