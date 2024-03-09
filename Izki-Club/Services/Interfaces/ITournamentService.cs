using Izki_Club.Dtos.MemberDtos;
using Izki_Club.Dtos.PlayerDtos;
using Izki_Club.Dtos.TournamentDtos;

namespace Izki_Club.Services.Interfaces
{
    public interface ITournamentService
    {
        Task<ApiResponse<string>> CreateTournament(AddTournamentDto input);
        Task<ApiResponse<PaginatedList<ViewTournamentDto>>> GetTournaments(GetTournamentDto input);
    }
}
