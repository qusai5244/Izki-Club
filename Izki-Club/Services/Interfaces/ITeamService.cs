using Izki_Club.Dtos.GeneralDtos;
using Izki_Club.Dtos.PlayerDtos;
using Izki_Club.Dtos.TeamDtos;
using static Izki_Club.Helpers.Enum;

namespace Izki_Club.Services.Interfaces
{
    public interface ITeamService
    {
        Task<ApiResponse<PaginatedList<ViewTeamDto>>> GetTeams(SearchAndPaginationDto input);
        Task<ApiResponse<ViewTeamDto>> GetTeam(int Id);
        Task<ApiResponse<ViewTeamDto>> CreateTeam(AddTeamDto input);
        Task<ApiResponse<ViewTeamDto>> UpdateTeam(int Id, UpdateTeamDto input);
        Task<ApiResponse<ViewTeamDto>> DeleteTeam(int Id);
    }
}
