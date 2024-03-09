using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Izki_Club.Dtos.OrganisationDto
{
    public class AddOrganizationDto
    {
        [Required]
        [StringLength(50)]
        public string NameEn { get; set; }
        [Required]
        [StringLength(50)]
        public string NameAr { get; set; }

        [StringLength(500)]
        [Column(TypeName = "varchar(500)")]
        public string DescriptionEn { get; set; }

        [StringLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string DescriptionAr { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
