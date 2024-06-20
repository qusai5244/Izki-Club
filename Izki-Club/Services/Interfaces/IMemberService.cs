using Izki_Club.Dtos.MemberDtos;
using Izki_Club.Dtos.PlayerDtos;

namespace Izki_Club.Services.Interfaces
{
    public interface IMemberService
    {
        Task<ApiResponse<int>> CreateMember(AddMemberDto input);
        Task<ApiResponse<PaginatedList<ViewMemberDto>>> GetMembers(ViewMembersByType input);
        Task<ApiResponse<ViewMemberDto>> GetMember(int Id);
        Task<ApiResponse<bool>> DeleteMember(int Id);
        Task<ApiResponse<bool>> UpdateMember(UpdateMemberDto input);


    }
}
