namespace Izki_Club.Models
{
    public class MatchTeam
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }

    }
}
