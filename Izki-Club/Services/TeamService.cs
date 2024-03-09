using Izki_Club.Data;
using Izki_Club.Dtos.GeneralDtos;
using Izki_Club.Dtos.PlayerDtos;
using Izki_Club.Dtos.TeamDtos;
using Izki_Club.Enums.General;
using Izki_Club.Helpers;
using Izki_Club.Models;
using Izki_Club.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Izki_Club.Services
{
    public class TeamService : ITeamService
    {

        private readonly DataContext _context;
        public TeamService(DataContext context)
        {
            _context = context;
        }


        public async Task<ApiResponse<ViewTeamDto>> CreateTeam(AddTeamDto input)
        {
            try
            {
                var organization = await _context.Organizations.FirstOrDefaultAsync(o=> o.Id == input.OrganizationId);

                if (organization == null)
                {
                    return new ApiResponse<ViewTeamDto>(false, (int)ResponseCodeEnum.NotFound, $"Organization not found", null);

                }

                var team = Mapper.TeamDtoToTeam(input);

                _context.Teams.Add(team);

                await _context.SaveChangesAsync();

                var teamDto = Mapper.TeamToTeamDto(team);

                return new ApiResponse<ViewTeamDto>(true, 200, "Member created successfully", teamDto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ViewTeamDto>(false, 500, $"An error occurred in Creatint Person: {ex}", null);
            }
        }

        public async Task<ApiResponse<ViewTeamDto>> DeleteTeam(int Id)
        {
            try
            {
                var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == Id);

                if (team == null)
                {
                    return new ApiResponse<ViewTeamDto>(false, (int)ResponseCodeEnum.NotFound, "Team not found", null);
                }

                team.IsDeleted = true;
                team.UpdatedAt = DateTime.UtcNow;
                _context.Teams.Update(team);
                await _context.SaveChangesAsync();

                var teamDto = Mapper.TeamToTeamDto(team);

                return new ApiResponse<ViewTeamDto>(true, (int)ResponseCodeEnum.Success, "Team deleted successfully", teamDto);

            }
            catch(Exception ex)
            {
                return new ApiResponse<ViewTeamDto>(false, 500, $"An error occurred in Creatint Person: {ex}", null);
            }
        }

        public async Task<ApiResponse<ViewTeamDto>> GetTeam(int Id)
        {
            try
            {
                var team = await _context.Teams.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);

                if (team == null)
                {
                    return new ApiResponse<ViewTeamDto>(false, (int)ResponseCodeEnum.NotFound, "Team not found", null);
                }

                var teamDto = Mapper.TeamToTeamDto(team);

                return new ApiResponse<ViewTeamDto>(true, (int)ResponseCodeEnum.Success, "Team data retrieved successfully", teamDto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ViewTeamDto>(false, (int)ResponseCodeEnum.InternalServerError, $"Error in GetTeam: {ex.Message}", null);
            }

        }

        public async Task<ApiResponse<PaginatedList<ViewTeamDto>>> GetTeams(SearchAndPaginationDto input)
        {
            try
            {
                var query = _context.Teams.AsNoTracking().AsQueryable();

                var filteredQuery = FilterQuery(query, input);

                var paginatedList = await GetTeamDtoListPaginated(filteredQuery, input);

                var response = new ApiResponse<PaginatedList<ViewTeamDto>>(true, 200, "Teams data retrieved successfully", paginatedList);

                return response;
            }
            catch (Exception ex)
            {
                // Log the exception for further analysis
                Console.WriteLine($"Error in GetTeams: {ex.Message}");

                return new ApiResponse<PaginatedList<ViewTeamDto>>(false, 500, $"Error in GetTeams: {ex.Message}", null);
            }
        }

        public async Task<ApiResponse<ViewTeamDto>> UpdateTeam(int Id,UpdateTeamDto input)
        {
            try
            {
                var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == Id);

                if (team == null)
                {
                    return new ApiResponse<ViewTeamDto>(false, (int)ResponseCodeEnum.NotFound, "Team not found", null);
                }

                team.NameAr = input.NameAr;
                team.NameEn = input.NameEn;
                team.DescriptionAr = input.DescriptionAr;
                team.DescriptionEn = input.DescriptionEn;
                team.IsActive = input.IsActive;
                team.FoundDate = input.FoundDate;
                team.ImageUrl = ImageProcess.UploadImage(input.Image);
                team.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var teamDto = Mapper.TeamToTeamDto(team);

                return new ApiResponse<ViewTeamDto>(true, (int)ResponseCodeEnum.Success, "Team updated successfully", teamDto);

            } 
            catch (Exception ex)
            {
                return new ApiResponse<ViewTeamDto>(false, 500, $"An error occurred in Creatint Person: {ex}", null);
            }
        }

        private IQueryable<Team> FilterQuery(IQueryable<Team> query, SearchAndPaginationDto input)
        {


            if (input.Search is not null)
            {
                query = query
                        .Where(t => t.NameAr.Contains(input.Search)
                                    || t.NameEn.Contains(input.Search));
            }

            return query;
        }
        private async Task<PaginatedList<ViewTeamDto>> GetTeamDtoListPaginated(IQueryable<Team> query, SearchAndPaginationDto input)
        {
            var teamsToReturn =  await query
                .Skip((input.Page - 1) * input.PageSize)
                .Take(input.PageSize)
                .Select(team => Mapper.TeamToTeamDto(team))
                .ToListAsync();

            return new PaginatedList<ViewTeamDto>(teamsToReturn, teamsToReturn.Count(), input.Page, input.PageSize);
        }


    }
}
