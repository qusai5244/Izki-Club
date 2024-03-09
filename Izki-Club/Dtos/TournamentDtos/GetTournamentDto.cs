using Izki_Club.Dtos.GeneralDtos;
using static Izki_Club.Enums.Tournament.TournamentEnum;

namespace Izki_Club.Dtos.TournamentDtos
{
    public class GetTournamentDto : SearchAndPaginationDto
    {
        public TournamentStatus? TournamentStatus { get; set; }
    }
}
