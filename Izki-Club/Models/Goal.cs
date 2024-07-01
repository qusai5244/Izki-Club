using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Izki_Club.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public int MatchId {  get; set; }
        public int PlayerId {  get; set; }
        [StringLength(500)] 
        [Column(TypeName = "varchar(500)")] 
        public string DescriptionEn { get; set; }
        [StringLength(500)]
        [Column(TypeName = "varchar(500)")]
        public string DescriptionAr { get; set; }
        public int Time { get; set; }
        public GoalType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Match Match { get; set; }
        [ForeignKey(nameof(PlayerId))]
        public Member Player { get; set; }


    }

    public enum GoalType
    {
        Normal = 1,
        OwnGoal = 2,
        FreeKick = 3,
        Penality = 4,
    }
}
