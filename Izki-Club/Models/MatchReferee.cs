namespace Izki_Club.Models
{
    public class MatchReferee
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }        
        public int RefereeId { get; set; }
        public Member Referee { get; set; }
    }
}
