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
        public MemberService(DataContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<ViewMemberDto>> CreateMember(AddMemberDto input)
        {
            try
            {

                if (input.TeamId is not 0)
                {
                    var team = await _context.Teams.AnyAsync(x => x.Id == input.TeamId && !x.IsDeleted);

                    if (!team)
                    {
                        return new ApiResponse<ViewMemberDto>(false, (int)ResponseCodeEnum.NotFound, "Team not found", null);
                    }
                }

                var member = Mapper.MemberDtoToMember(input);

                _context.Members.Add(member);

                await _context.SaveChangesAsync();

                var memberDto = Mapper.MemberToMemberDto(member);

                return new ApiResponse<ViewMemberDto>(true, (int)ResponseCodeEnum.Success, "Member created successfully", memberDto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ViewMemberDto>(false, (int)ResponseCodeEnum.InternalServerError, $"An error occurred in Creating Member: {ex}", null);
            }
        }

        public async Task<ApiResponse<ViewMemberDto>> DeleteMember(int id)
        {
            try
            {
                var member = await _context.Members
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (member == null)
                {
                    return new ApiResponse<ViewMemberDto>(false, (int)ResponseCodeEnum.NotFound, "Member not found", null);
                }

                // Soft delete the member by setting IsDeleted to true
                member.IsDeleted = true;

                // Save changes to the database
                _context.Members.Update(member);
                await _context.SaveChangesAsync();

                return new ApiResponse<ViewMemberDto>(true, (int)ResponseCodeEnum.Success, "Member deleted successfully", null);
            }
            catch (Exception ex)
            {
                // Log the exception for further analysis
                Console.WriteLine($"An error occurred in Deleting Member: {ex}");
                return new ApiResponse<ViewMemberDto>(false, (int)ResponseCodeEnum.InternalServerError, "An error occurred while deleting the member", null);
            }
        }


        public async Task<ApiResponse<ViewMemberDto>> GetMember(int Id)
        {
            try
            {
                var member = await _context.Members.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

                if (member == null)
                {
                    return new ApiResponse<ViewMemberDto>(false, (int)ResponseCodeEnum.NotFound, "Member not found", null);
                }

                var memberDto = Mapper.MemberToMemberDto(member);

                return new ApiResponse<ViewMemberDto>(true, (int)ResponseCodeEnum.Success, "Member retrived succssefuly", memberDto);

            }
            catch (Exception ex)
            {
                return new ApiResponse<ViewMemberDto>(false, (int)ResponseCodeEnum.InternalServerError, $"An error occurred in Creating Member: {ex}", null);   
            }
        }

        public async Task<ApiResponse<PaginatedList<ViewMemberDto>>> GetMembers(ViewMembersByType input)
        {
            try
            {
                var query = _context.Members.Where(x => !x.IsDeleted).AsNoTracking().AsQueryable();

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

                var membersToReturn = await query
                    .Skip((input.Page - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .Select(member => Mapper.MemberToMemberDto(member))
                    .ToListAsync();

                var membersPaginatedList = new PaginatedList<ViewMemberDto>(membersToReturn, membersToReturn.Count(), input.Page, input.PageSize);

                return new ApiResponse<PaginatedList<ViewMemberDto>>(true, (int)ResponseCodeEnum.Success, "Members retrived succssefuly", membersPaginatedList);

            } 
            catch (Exception ex)
            {
                return new ApiResponse<PaginatedList<ViewMemberDto>>(false, (int)ResponseCodeEnum.InternalServerError, $"An error occurred in GetMembers: {ex}", null);
            }
        }

        public async Task<ApiResponse<ViewMemberDto>> UpdateMember(int id, UpdateMemberDto input)
        {
            try
            {
                // Retrieve the member with the specified ID that is not deleted
                var member = await _context.Members.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (member == null)
                {
                    return new ApiResponse<ViewMemberDto>(false, (int)ResponseCodeEnum.NotFound, "Member not found", null);
                }

                // Check if the team exists and is not deleted
                var teamExists = await _context.Teams.AnyAsync(x => x.Id == input.TeamId && !x.IsDeleted);

                if (!teamExists)
                {
                    return new ApiResponse<ViewMemberDto>(false, (int)ResponseCodeEnum.NotFound, "Team not found", null);
                }

                // Delete the old image after checking if the user is found
                ImageProcess.DeleteImage(member.ImageUrl);

                // Update user information
                member.NameAr = input.NameAr;
                member.NameEn = input.NameEn;
                member.DescriptionAr = input.DescriptionAr;
                member.DescriptionEn = input.DescriptionEn;
                member.DateOfBirth = input.DateOfBirth;
                member.ImageUrl = ImageProcess.UploadImage(input.Image);
                member.TeamId = input.TeamId;
                member.UpdatedAt = DateTime.Now;

                // Save changes to the database
                _context.Members.Update(member);
                await _context.SaveChangesAsync();

                // Return ApiResponse with a success status code and updated player information
                var updatedMemberDto = Mapper.MemberToMemberDto(member);
                return new ApiResponse<ViewMemberDto>(true, (int)ResponseCodeEnum.Success, "Member updated successfully", updatedMemberDto);
            }
            catch (Exception ex)
            {
                // Log the exception for further analysis
                Console.WriteLine($"An error occurred in Update Member: {ex}");
                return new ApiResponse<ViewMemberDto>(false, (int)ResponseCodeEnum.InternalServerError, "An error occurred while updating the member", null);
            }
        }


    }
}
