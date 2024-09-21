using static Izki_Club.Enums.Member.MemberTypeEnum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Izki_Club.Models
{
    public class Team
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
        [Required]
        public DateTime FoundDate{ get; set; }
        [Required]
        public TeamStatus Status { get; set; } = TeamStatus.Active;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public ICollection<TournamentTeam> TournamentTeams { get; set; }
    }

    public enum TeamStatus
    {
        Active = 1,
        Inactive = 2,
        Deleted = 3,
    }
}
