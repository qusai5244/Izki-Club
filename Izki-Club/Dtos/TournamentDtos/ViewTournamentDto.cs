using Izki_Club.Models;
using static Izki_Club.Enums.Tournament.TournamentEnum;
using System.ComponentModel.DataAnnotations;

namespace Izki_Club.Dtos.TournamentDtos
{
    public class ViewTournamentDto
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TournamentStatus tournamentStatus { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
