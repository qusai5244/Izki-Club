using Izki_Club.Data;
using Izki_Club.Dtos.GeneralDtos;
using Izki_Club.Dtos.OrganisationDto;
using Izki_Club.Dtos.RefereeDtos;
using Izki_Club.Enums.General;
using Izki_Club.Helpers;
using Izki_Club.Models;
using Izki_Club.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Izki_Club.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly DataContext _context;
        public OrganizationService(DataContext context)
        {
            _context = context;
        }

        //public async Task<ApiResponse<PaginatedList<ViewRefereeDto>>> GetReferees(SearchAndPaginationDto input)
        //{
        //    try
        //    {
        //        var query = _context.Referees
        //            .AsNoTracking()
        //            .AsQueryable();

        //        if (!string.IsNullOrEmpty(input.SearchEn))
        //        {
        //            query = query.Where(x => x.NameEn.Contains(input.SearchEn));
        //        }

        //        if (!string.IsNullOrEmpty(input.SearchAr))
        //        {
        //            query = query.Where(x => x.NameAr.Contains(input.SearchAr));
        //        }

        //        var refereesToReturn = await query.Select(r => new ViewRefereeDto
        //        {
        //            Id = r.Id,
        //            NameEn = r.NameEn,
        //            NameAr = r.NameAr,
        //            IsActive = r.IsActive,
        //            IsDeleted = r.IsDeleted,
        //            Age = Mapper.CalculateAge(r.DateOfBirth),
        //            Image = r.ImageUrl,
        //            CreatedAt = r.CreatedAt,
        //            UpdatedAt = r.UpdatedAt,
        //        })
        //        .ToListAsync();

        //        var refereesPaginatedList = new PaginatedList<ViewRefereeDto>(refereesToReturn, refereesToReturn.Count(), input.Page, input.PageSize);

        //        return new ApiResponse<PaginatedList<ViewRefereeDto>>(true, (int)ResponseCodeEnum.Success, "referees retrived succssefuly", refereesPaginatedList);

        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions appropriately (log, return specific ApiResponse, etc.)
        //        return new ApiResponse<PaginatedList<ViewRefereeDto>>(false, (int)ResponseCodeEnum.InternalServerError, ex.Message, null);
        //    }
        //}

        public async Task<ApiResponse<ViewOrganizationDto>> CreatOrganization(AddOrganizationDto input)
        {
            try
            {

                var organisation = new Organization
                {
                    NameAr = input.NameAr,
                    NameEn = input.NameEn,
                    DescriptionAr = input.DescriptionAr,
                    DescriptionEn = input.DescriptionEn,
                    ImageUrl = input.ImageUrl,
                };

                _context.Organizations.Add(organisation);

                await _context.SaveChangesAsync();

                var refereeDto = new ViewOrganizationDto
                {
                    Id = organisation.Id,
                    NameEn = organisation.NameEn,
                    NameAr = organisation.NameAr,
                    DescriptionEn = organisation.DescriptionEn,
                    DescriptionAr = organisation.DescriptionAr,
                    IsActive = organisation.IsActive,
                    IsDeleted = organisation.IsDeleted,
                    ImageUrl = organisation.ImageUrl,
                    CreatedAt = organisation.CreatedAt,
                    UpdatedAt = organisation.UpdatedAt,
                };

                return new ApiResponse<ViewOrganizationDto>(true, (int)ResponseCodeEnum.Success, "Organization created successfully", refereeDto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ViewOrganizationDto>(false, (int)ResponseCodeEnum.InternalServerError, $"An error occurred in Creating Organization: {ex}", null);
            }
        }



    }
}
