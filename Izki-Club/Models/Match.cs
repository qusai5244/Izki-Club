using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Izki_Club.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar(255)")]
        public string Description {  get; set; }
        public string DescriptionAr {  get; set; }
        public MatchStatus Status {  get; set; }
        public MatchRound Round {  get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Goal> Goals { get; set; }
        public ICollection<Card> Cards { get; set; }
        public ICollection<Penality> Penalities { get; set; }

    }

    public enum MatchStatus 
    {
        UpComing = 1,
        Active = 2,
        Completed = 3,
    }

    public enum MatchRound
    {
        Groups = 1,
        Round16 = 2,
        QuarterFinals = 3,
        SemiFinals = 4,
        Finals = 5,
    }


}
