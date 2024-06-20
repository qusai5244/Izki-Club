using System.ComponentModel.DataAnnotations;

namespace Izki_Club.Dtos.TournamentDtos
{
    public class GetSingleTournamentDto
    {
        [Required]
        public int Id { get; set; }
    }
}
