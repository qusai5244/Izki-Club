using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Izki_Club.Models
{
    public class Tournament
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
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TournamentStatus Status { get; set; } = TournamentStatus.Upcoming;
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public ICollection<TournamentTeam> TournamentTeams { get; set; }
    }

    public enum TournamentStatus
    {
        Upcoming = 1,
        Active,
        Finished
    }
}
