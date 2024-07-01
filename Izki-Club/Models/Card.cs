using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Izki_Club.Models
{
    public class Card
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public int Time { get; set; }
        public CardType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Match Match { get; set; }
        [ForeignKey(nameof(PlayerId))]
        public Member Player { get; set; }
    }

    public enum CardType
    {
        Yellow = 1,
        Red = 2,
    }
}
