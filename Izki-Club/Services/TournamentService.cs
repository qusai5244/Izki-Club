using Izki_Club.Data;
using Izki_Club.Dtos.GeneralDtos;
using Izki_Club.Dtos.MemberDtos;
using Izki_Club.Dtos.PlayerDtos;
using Izki_Club.Dtos.TeamDtos;
using Izki_Club.Dtos.TournamentDtos;
using Izki_Club.Enums.General;
using Izki_Club.Helpers;
using Izki_Club.Models;
using Izki_Club.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Izki_Club.Enums.Member.MemberTypeEnum;

namespace Izki_Club.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly DataContext _context;
        public TournamentService(DataContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<string>> CreateTournament(AddTournamentDto input)
        {
            try
            {
                var organization = await _context
                                        .Organizations
                                        .Where(a=> a.Id == input.OrganizationId)
                                        .FirstOrDefaultAsync();

                if (organization == null)
                {
                    return new ApiResponse<string>(false, (int)ResponseCodeEnum.NotFound, "Organization not found", null);
                }

                var tournament = new Tournament
                {
                    NameEn = input.NameEn,
                    NameAr = input.NameAr,
                    DescriptionEn = input.DescriptionEn,
                    DescriptionAr = input.DescriptionAr,
                    StartDate = input.StartDate,
                    ImageUrl = input.ImageUrl,
                    OrganizationId = input.OrganizationId,
                };

                await _context.Tournaments.AddAsync(tournament);
                await _context.SaveChangesAsync();

                return new ApiResponse<string>(true, (int)ResponseCodeEnum.Success, "Tournament Created Successfully", null);

            }
            catch (Exception ex)
            {
                return new ApiResponse<string>(false, (int)ResponseCodeEnum.InternalServerError, $"An error occurred in Creating Member: {ex}", null);
            }
        }

        public async Task<ApiResponse<PaginatedList<ViewTournamentDto>>> GetTournaments(GetTournamentDto input)
        {
            try
            {
                var query = _context
                            .Tournaments
                            .AsNoTracking()
                            .AsQueryable();

                if (input.Search is not null)
                {
                    query = query
                            .Where(t => t.NameAr.Contains(input.Search)
                                        || t.NameEn.Contains(input.Search));
                }

                if (input.TournamentStatus.HasValue)
                {
                    query = query
                            .Where(t => t.tournamentStatus == input.TournamentStatus);
                }

                var totalCount = await query.CountAsync();

                var tournaments = await query
                                        .Skip((input.Page - 1) * input.PageSize)
                                        .Take(input.PageSize)
                                        .Select(t => new ViewTournamentDto
                                        {
                                            Id = t.Id,
                                            NameAr = t.NameAr,
                                            NameEn = t.NameEn,
                                            DescriptionAr = t.DescriptionAr,
                                            DescriptionEn = t.DescriptionEn,
                                            StartDate = t.StartDate,
                                            EndDate = t.EndDate,
                                            tournamentStatus = t.tournamentStatus,
                                            ImageUrl = t.ImageUrl,
                                            CreatedAt = t.CreatedAt,
                                            UpdatedAt = t.UpdatedAt,
                                        })
                                        .ToListAsync();

                var paginatedList = new PaginatedList<ViewTournamentDto>(tournaments, totalCount, input.Page, input.PageSize);

                var response = new ApiResponse<PaginatedList<ViewTournamentDto>>(true, 200, "Tournaments data retrieved successfully", paginatedList);

                return response;
            }
            catch (Exception ex)
            {
                return new ApiResponse<PaginatedList<ViewTournamentDto>>(false, 500, $"Error in GetTournaments: {ex.Message}", null);
            }
        }
        public async Task<ApiResponse<ViewTournamentDto>> GetTournament(int id)
        {
            try
            {
                var tournament = await _context
                                        .Tournaments
                                        .AsNoTracking()
                                        .Where(t => t.Id == id)
                                        .Select(t => new ViewTournamentDto
                                        {
                                            Id = t.Id,
                                            NameAr = t.NameAr,
                                            NameEn = t.NameEn,
                                            DescriptionAr = t.DescriptionAr,
                                            DescriptionEn = t.DescriptionEn,
                                            StartDate = t.StartDate,
                                            EndDate = t.EndDate,
                                            tournamentStatus = t.tournamentStatus,
                                            ImageUrl = t.ImageUrl,
                                            CreatedAt = t.CreatedAt,
                                            UpdatedAt = t.UpdatedAt,
                                        })
                                        .FirstOrDefaultAsync();

                if (tournament == null)
                {
                    return new ApiResponse<ViewTournamentDto>(false, (int)ResponseCodeEnum.NotFound,  "Tournament Not Found", null);
                }

                var response = new ApiResponse<ViewTournamentDto>(true, 200, "Tournaments data retrieved successfully", tournament);

                return response;
            }
            catch (Exception ex)
            {
                return new ApiResponse<ViewTournamentDto>(false, 500, $"Error in GetTournaments: {ex.Message}", null);
            }
        }



    }
}
