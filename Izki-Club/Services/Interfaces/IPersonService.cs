using Izki_Club.Dtos.PlayerDtos;
using static Izki_Club.Helpers.Enum;

namespace Izki_Club.Services.Interfaces
{
    public interface IPersonService
    {
        Task<ApiResponse<PaginatedList<ViewPersonDto>>> GetPersons(string searchInput, PersonType personType, int page, int pageSize);
        Task<ApiResponse<ViewPersonDto>> GetPerson(int Id);
        Task<ApiResponse<ViewPersonDto>> CreatePerson(AddPersonDto input);
        Task<ApiResponse<ViewPersonDto>> UpdatePerson(UpdatePersonDto input);
        Task<ApiResponse<ViewPersonDto>> DeletePerson(int Id);
        Task<ApiResponse<ViewPersonDto>> UpdatePersonStatus(int Id);

    }
}
