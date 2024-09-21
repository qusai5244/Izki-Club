using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Izki_Club.Models
{
    public class Member
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(255)")]
        public string NameEn { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(255)")]
        public string NameAr { get; set; }

        [StringLength(500)] 
        [Column(TypeName = "nvarchar(500)")] 
        public string DescriptionEn { get; set; }

        [StringLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string DescriptionAr { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public MemberStatus Status { get; set; } = MemberStatus.Active;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public MemberType Type { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }

    }

    public enum MemberType
    {
        Player = 1,
        Coach = 2,
        Referee = 3,
    }

    public enum MemberStatus
    {
        Active = 1,
        Inactive = 2,
        Deleted = 3,
    }   
}
