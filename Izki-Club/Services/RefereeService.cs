using Izki_Club.Data;
using Izki_Club.Dtos.GeneralDtos;
using Izki_Club.Dtos.PlayerDtos;
using Izki_Club.Dtos.RefereeDtos;
using Izki_Club.Enums.General;
using Izki_Club.Helpers;
using Izki_Club.Models;
using Izki_Club.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Izki_Club.Services
{
    public class RefereeService : IRefereeService
    {
        private readonly DataContext _context;
        public RefereeService(DataContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<PaginatedList<ViewRefereeDto>>> GetReferees(SearchAndPaginationDto input)
        {
            try
            {
                var query = _context.Referees
                    .AsNoTracking()
                    .AsQueryable();

                if (!string.IsNullOrEmpty(input.SearchEn))
                {
                    query = query.Where(x => x.NameEn.Contains(input.SearchEn));
                }

                if (!string.IsNullOrEmpty(input.SearchAr))
                {
                    query = query.Where(x => x.NameAr.Contains(input.SearchAr));
                }

                var refereesToReturn = await query.Select(r => new ViewRefereeDto
                {
                    Id = r.Id,
                    NameEn = r.NameEn,
                    NameAr = r.NameAr,
                    IsActive = r.IsActive,
                    Age = Mapper.CalculateAge(r.DateOfBirth),
                    Image = r.ImageUrl,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                })
                .ToListAsync();

                var refereesPaginatedList = new PaginatedList<ViewRefereeDto>(refereesToReturn, refereesToReturn.Count(), input.Page, input.PageSize);

                return new ApiResponse<PaginatedList<ViewRefereeDto>>(true, (int)ResponseCodeEnum.Success, "referees retrived succssefuly", refereesPaginatedList);

            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (log, return specific ApiResponse, etc.)
                return new ApiResponse<PaginatedList<ViewRefereeDto>>(false, (int)ResponseCodeEnum.InternalServerError, ex.Message, null);
            }
        }

        public async Task<ApiResponse<ViewRefereeDto>> GetReferee(int Id)
        {
            try
            {
                var refereeDto = await _context.Referees
                    .Where(x => x.Id == Id && !x.IsDeleted)
                    .Select(r => new ViewRefereeDto
                    {
                        Id = r.Id,
                        NameEn = r.NameEn,
                        NameAr = r.NameAr,
                        IsActive = r.IsActive,
                        Age = Mapper.CalculateAge(r.DateOfBirth),
                        Image = r.ImageUrl,
                        CreatedAt = r.CreatedAt,
                        UpdatedAt = r.UpdatedAt,
                    })
                    .FirstOrDefaultAsync();

                if (refereeDto == null)
                {
                    return new ApiResponse<ViewRefereeDto>(false, (int)ResponseCodeEnum.NotFound, "Referee not found", null);
                }

                return new ApiResponse<ViewRefereeDto>(true, (int)ResponseCodeEnum.Success, "Referee retrived succssefuly", refereeDto);

            }
            catch (Exception ex)
            {
                return new ApiResponse<ViewRefereeDto>(false, (int)ResponseCodeEnum.InternalServerError, $"An error occurred in Creating Referee: {ex}", null);
            }
        }

        public async Task<ApiResponse<ViewRefereeDto>> CreatReferee(AddRefereeDto input)
        {
            try
            {
                var organisation = _context.Organizations.FirstOrDefault(o => o.Id == input.OrganizationId);

                if (organisation == null)
                {
                    return new ApiResponse<ViewRefereeDto>(false, (int)ResponseCodeEnum.NotFound, $"organisation not found", null);

                }
                var referee = new Referee
                {
                    NameAr = input.NameAr,
                    NameEn = input.NameEn,
                    DescriptionAr = input.DescriptionAr,
                    DescriptionEn = input.DescriptionEn,
                    DateOfBirth = input.DateOfBirth,
                    ImageUrl = input.ImageUrl,
                    IsActive = true,
                    IsDeleted = false,
                    OrganizationId = input.OrganizationId
                };

                _context.Referees.Add(referee);

                await _context.SaveChangesAsync();

                var refereeDto = new ViewRefereeDto
                {
                    Id = referee.Id,
                    NameEn = referee.NameEn,
                    NameAr = referee.NameAr,
                    IsActive = referee.IsActive,
                    Age = Mapper.CalculateAge(referee.DateOfBirth),
                    Image = referee.ImageUrl,
                    CreatedAt = referee.CreatedAt,
                    UpdatedAt = referee.UpdatedAt,
                };

                return new ApiResponse<ViewRefereeDto>(true, (int)ResponseCodeEnum.Success, "Referee created successfully", refereeDto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ViewRefereeDto>(false, (int)ResponseCodeEnum.InternalServerError, $"An error occurred in Creating Referee: {ex}", null);
            }
        }

        public async Task<ApiResponse<ViewRefereeDto>> UpdateReferee(int id, UpdateRefereeDto input)
        {
            try
            {
                var referee = await _context.Referees.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (referee == null)
                {
                    return new ApiResponse<ViewRefereeDto>(false, (int)ResponseCodeEnum.NotFound, "Referee not found", null);
                }


                // Delete the old image after checking if the user is found
                ImageProcess.DeleteImage(referee.ImageUrl);

                // Update user information
                referee.NameAr = input.NameAr;
                referee.NameEn = input.NameEn;
                referee.DescriptionAr = input.DescriptionAr;
                referee.DescriptionEn = input.DescriptionEn;
                referee.DateOfBirth = input.DateOfBirth;
                referee.ImageUrl = input.ImageUrl;
                referee.IsActive = input.IsActive;
                referee.UpdatedAt = DateTime.Now;

                // Save changes to the database
                _context.Referees.Update(referee);
                await _context.SaveChangesAsync();

                var updatedRefereeDto = new ViewRefereeDto
                {
                    Id = referee.Id,
                    NameEn = referee.NameEn,
                    NameAr = referee.NameAr,
                    IsActive = referee.IsActive,
                    Age = Mapper.CalculateAge(referee.DateOfBirth),
                    Image = referee.ImageUrl,
                    CreatedAt = referee.CreatedAt,
                    UpdatedAt = referee.UpdatedAt,
                };
                // Return ApiResponse with a success status code and updated player information
                return new ApiResponse<ViewRefereeDto>(true, (int)ResponseCodeEnum.Success, "Referee updated successfully", updatedRefereeDto);
            }
            catch (Exception ex)
            {
                // Log the exception for further analysis
                Console.WriteLine($"An error occurred in Update Member: {ex}");
                return new ApiResponse<ViewRefereeDto>(false, (int)ResponseCodeEnum.InternalServerError, "An error occurred while updating the referee", null);
            }
        }

        public async Task<ApiResponse<ViewRefereeDto>> DeleteReferee(int id)
        {
            try
            {
                var referee = await _context.Referees
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (referee == null)
                {
                    return new ApiResponse<ViewRefereeDto>(false, (int)ResponseCodeEnum.NotFound, "Referee not found", null);
                }

                // Soft delete the member by setting IsDeleted to true
                referee.IsDeleted = true;

                var deletedReferee = new ViewRefereeDto
                {
                    Id = referee.Id,
                    NameEn = referee.NameEn,
                    NameAr = referee.NameAr,
                    IsActive = referee.IsActive,
                    Age = Mapper.CalculateAge(referee.DateOfBirth),
                    Image = referee.ImageUrl,
                    CreatedAt = referee.CreatedAt,
                    UpdatedAt = referee.UpdatedAt,
                };

                // Save changes to the database
                _context.Referees.Update(referee);
                await _context.SaveChangesAsync();

                return new ApiResponse<ViewRefereeDto>(true, (int)ResponseCodeEnum.Success, "Referee deleted successfully", deletedReferee);
            }
            catch (Exception ex)
            {
                // Log the exception for further analysis
                Console.WriteLine($"An error occurred in Deleting Member: {ex}");
                return new ApiResponse<ViewRefereeDto>(false, (int)ResponseCodeEnum.InternalServerError, "An error occurred while deleting the Referee", null);
            }
        }



    }
}
