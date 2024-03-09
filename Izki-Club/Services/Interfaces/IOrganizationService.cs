using Izki_Club.Dtos.OrganisationDto;

namespace Izki_Club.Services.Interfaces
{
    public interface IOrganizationService
    {
        Task<ApiResponse<ViewOrganizationDto>> CreatOrganization(AddOrganizationDto input);
    }
}
