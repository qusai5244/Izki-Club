using Izki_Club.Dtos.baseDtos;
using System.ComponentModel.DataAnnotations;

namespace Izki_Club.Dtos.TournamentDtos
{
    public class AddTournamentDto : BaseDto
    {
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public int OrganizationId { get; set; }
    };
}
