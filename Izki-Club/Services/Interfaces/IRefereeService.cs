using Izki_Club.Dtos.GeneralDtos;
using Izki_Club.Dtos.MemberDtos;
using Izki_Club.Dtos.PlayerDtos;
using Izki_Club.Dtos.RefereeDtos;

namespace Izki_Club.Services.Interfaces
{
    public interface IRefereeService
    {
        Task<ApiResponse<ViewRefereeDto>> CreatReferee(AddRefereeDto input);
        Task<ApiResponse<ViewRefereeDto>> DeleteReferee(int id);
        Task<ApiResponse<ViewRefereeDto>> GetReferee(int Id);
        Task<ApiResponse<PaginatedList<ViewRefereeDto>>> GetReferees(SearchAndPaginationDto input);
        Task<ApiResponse<ViewRefereeDto>> UpdateReferee(int id, UpdateRefereeDto input);
    }
}
