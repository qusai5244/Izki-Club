using System.ComponentModel.DataAnnotations;

namespace Izki_Club.Models
{
    public class TournamentTeam
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
        [Required]
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
