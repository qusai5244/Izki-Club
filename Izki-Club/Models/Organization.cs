using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Izki_Club.Models
{
    public class Organization
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(255)")]
        public string NameEn { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(255)")]
        public string NameAr { get; set; }

        [StringLength(500)]
        [Column(TypeName = "varchar(500)")]
        public string DescriptionEn { get; set; }

        [StringLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string DescriptionAr { get; set; }
        public string ImageUrl { get; set; }
        public OrganizationStatus Status { get; set; } = OrganizationStatus.Inactive;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public ICollection<Team> Teams { get; set; }
        public ICollection<Tournament> Tournaments { get; set; }
    }

    public enum OrganizationStatus
    {
        Active = 1,
        Inactive = 2,
        Deleted = 3,
    }
}
