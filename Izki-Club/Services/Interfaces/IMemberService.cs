using Izki_Club.Dtos.MemberDtos;
using Izki_Club.Dtos.PlayerDtos;

namespace Izki_Club.Services.Interfaces
{
    public interface IMemberService
    {
        Task<ApiResponse<PaginatedList<ViewMemberDto>>> GetMembers(ViewMembersByType input);
        Task<ApiResponse<ViewMemberDto>> GetMember(int Id);
        Task<ApiResponse<ViewMemberDto>> CreateMember(AddMemberDto input);
        Task<ApiResponse<ViewMemberDto>> UpdateMember(int Id, UpdateMemberDto input);
        Task<ApiResponse<ViewMemberDto>> DeleteMember(int Id);

    }
}
