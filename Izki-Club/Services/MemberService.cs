using Izki_Club.Data;
using Izki_Club.Dtos.GeneralDtos;
using Izki_Club.Dtos.MemberDtos;
using Izki_Club.Dtos.PlayerDtos;
using Izki_Club.Dtos.TeamDtos;
using Izki_Club.Enums.General;
using Izki_Club.Helpers;
using Izki_Club.Models;
using Izki_Club.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Izki_Club.Enums.Member.MemberTypeEnum;

namespace Izki_Club.Services
{
    public class MemberService : IMemberService
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;
        public MemberService(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }
        public async Task<ApiResponse<int>> CreateMember(AddMemberDto input)
        {
            try
            {

                if (input.TeamId is not 0)
                {
                    var team = await _context
                                     .Teams
                                     .AnyAsync(x => x.Id == input.TeamId && !x.IsDeleted);

                    if (!team)
                    {
                        return new ApiResponse<int>(false, (int)ResponseCodeEnum.NotFound, "Team not found", 0);
                    }
                }

                var member = new Member
                {
                    MemberType = input.MemberType,
                    DateOfBirth = input.DateOfBirth,
                    NameEn = input.NameEn,
                    NameAr = input.NameAr,
                    DescriptionEn = input.DescriptionEn,
                    DescriptionAr = input.DescriptionAr,
                    ImageUrl = input.Image,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    TeamId = input.TeamId,
                };

                _context.Members.Add(member);

                await _context.SaveChangesAsync();

                return new ApiResponse<int>(true, (int)ResponseCodeEnum.Success, "Member created successfully", member.Id);
            }
            catch (Exception ex)
            {
                return new ApiResponse<int>(false, (int)ResponseCodeEnum.InternalServerError, $"An error occurred in Creating Member: {ex}", 0);
            }
        }
        public async Task<ApiResponse<PaginatedList<ViewMemberDto>>> GetMembers(ViewMembersByType input)
        {
            try
            {
                var query = _context
                            .Members
                            .Where(x => !x.IsDeleted)
                            .AsNoTracking()
                            .AsQueryable();

                if (input.Search is not null)
                {
                    query = query
                            .Where(t => t.NameAr.Contains(input.Search)
                                        || t.NameEn.Contains(input.Search));
                }

                if (input.memberType is not 0)
                {
                    query = query.Where(x => x.MemberType == input.memberType);
                }

                var totalCount = await query.CountAsync();

                var membersToReturn = await query
                    .Skip((input.Page - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .Select(member => new ViewMemberDto
                    {
                        Id = member.Id,
                        MemberType = member.MemberType.ToString(),
                        NameEn = member.NameEn,
                        NameAr = member.NameAr,
                        DescriptionEn = member.DescriptionEn,
                        DescriptionAr = member.DescriptionAr,
                        Image = member.ImageUrl,
                        IsActive = member.IsActive,
                        Age = _utilityService.CalculateAge(member.DateOfBirth),
                        TeamId = member.TeamId,
                        CreatedAt = member.CreatedAt,
                        UpdatedAt = member.UpdatedAt,
                    })
                    .ToListAsync();

                var membersPaginatedList = new PaginatedList<ViewMemberDto>(membersToReturn, totalCount, input.Page, input.PageSize);

                return new ApiResponse<PaginatedList<ViewMemberDto>>(true, (int)ResponseCodeEnum.Success, "Members retrived succssefuly", membersPaginatedList);

            }
            catch (Exception ex)
            {
                return new ApiResponse<PaginatedList<ViewMemberDto>>(false, (int)ResponseCodeEnum.InternalServerError, $"An error occurred in GetMembers: {ex}", null);
            }
        }
        public async Task<ApiResponse<ViewMemberDto>> GetMember(int Id)
        {
            try
            {
                var member = await _context
                                   .Members
                                   .FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

                if (member == null)
                {
                    return new ApiResponse<ViewMemberDto>(false, (int)ResponseCodeEnum.NotFound, "Member not found", null);
                }

                var memberDto = new ViewMemberDto
                {
                    Id = member.Id,
                    MemberType = member.MemberType.ToString(),
                    NameEn = member.NameEn,
                    NameAr = member.NameAr,
                    DescriptionEn = member.DescriptionEn,
                    DescriptionAr = member.DescriptionAr,
                    Image = member.ImageUrl,
                    IsActive = member.IsActive,
                    Age = _utilityService.CalculateAge(member.DateOfBirth),
                    TeamId = member.TeamId,
                    CreatedAt = member.CreatedAt,
                    UpdatedAt = member.UpdatedAt,
                };

                return new ApiResponse<ViewMemberDto>(true, (int)ResponseCodeEnum.Success, "Member retrived succssefuly", memberDto);

            }
            catch (Exception ex)
            {
                return new ApiResponse<ViewMemberDto>(false, (int)ResponseCodeEnum.InternalServerError, $"An error occurred in Creating Member: {ex}", null);
            }
        }
        public async Task<ApiResponse<bool>> DeleteMember(int id)
        {
            try
            {
                var member = await _context
                                   .Members
                                   .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (member == null)
                {
                    return new ApiResponse<bool>(false, (int)ResponseCodeEnum.NotFound, "Member not found", false);
                }

                member.IsDeleted = true;

                _context.Members.Update(member);
                await _context.SaveChangesAsync();

                return new ApiResponse<bool>(true, (int)ResponseCodeEnum.Success, "Member deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(false, (int)ResponseCodeEnum.InternalServerError, "An error occurred while deleting the member", false);
            }
        }
        public async Task<ApiResponse<bool>> UpdateMember(UpdateMemberDto input)
        {
            try
            {
                var member = await _context
                                   .Members
                                   .FirstOrDefaultAsync(x => x.Id == input.Id && !x.IsDeleted);

                if (member == null)
                {
                    return new ApiResponse<bool>(false, (int)ResponseCodeEnum.NotFound, "Member not found", false);
                }

                var teamExists = await _context
                                       .Teams
                                       .AnyAsync(x => x.Id == input.TeamId && !x.IsDeleted);

                if (!teamExists)
                {
                    return new ApiResponse<bool>(false, (int)ResponseCodeEnum.NotFound, "Team not found", false);
                }

                ImageProcess.DeleteImage(member.ImageUrl);

                member.NameAr = input.NameAr;
                member.NameEn = input.NameEn;
                member.DescriptionAr = input.DescriptionAr;
                member.DescriptionEn = input.DescriptionEn;
                member.DateOfBirth = input.DateOfBirth;
                member.ImageUrl = ImageProcess.UploadImage(input.Image);
                member.TeamId = input.TeamId;
                member.UpdatedAt = DateTime.Now;

                _context.Members.Update(member);
                await _context.SaveChangesAsync();

                return new ApiResponse<bool>(true, (int)ResponseCodeEnum.Success, "Member updated successfully", true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(false, (int)ResponseCodeEnum.InternalServerError, "An error occurred while updating the member", false);
            }
        }

    }
}
