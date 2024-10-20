using Izki_Club.Dtos.GeneralDtos;
using Izki_Club.Models;

namespace Izki_Club.Dtos.TournamentDtos
{
    public class GetTournamentDto : SearchAndPaginationDto
    {
        public TournamentStatus? TournamentStatus { get; set; }
    }
}
