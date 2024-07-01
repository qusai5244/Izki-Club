using System.ComponentModel.DataAnnotations.Schema;

namespace Izki_Club.Models
{
    public class MatchTeamPlayer
    {
        public int Id { get; set; }
        public int MatchTeamId { get; set; }
        public MatchTeam MatchTeam { get; set; }
        public int PlayerId { get; set; }
        [ForeignKey(nameof(PlayerId))]
        public Member Member { get; set; }
        public bool IsMain { get; set; }
    }
}
