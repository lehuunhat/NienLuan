using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HienTangToc.Models
{
    public class MTvsSalonModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MTvsSalonId { get; set; }
        [Required]
        public int idNM { get; set; }
        [Required]
        public int idSalon { get; set; }

        [ForeignKey("idNM")]
        public NguoiMuonModel NguoiMuonModel { get; set; }

        [ForeignKey("idSalon")]
        public SalonTocModel SalonTocModel { get; set; }
    }
}
