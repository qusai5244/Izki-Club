using Izki_Club.Data;
using Izki_Club.Dtos.PlayerDtos;
using Izki_Club.Helpers;
using Izki_Club.Models;
using Izki_Club.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Izki_Club.Helpers.Enum;

namespace Izki_Club.Services
{
    public class PersonService : IMemberService
    {
        private readonly DataContext _context;
        public PersonService(DataContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<ViewMemberDto>> CreateMember(AddMemberDto input)
        {
            try
            {
                var member = Mapper.MemberDtoToMember(input);

                _context.Members.Add(member);

                await _context.SaveChangesAsync();

                var memberDto = Mapper.MemberToMemberDto(member);

                return new ApiResponse<ViewMemberDto>(true, 200, "Member created successfully", memberDto);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred in Creating Member: {ex}");

                // Return ApiResponse with a 500 status code and an error message
                return new ApiResponse<ViewMemberDto>(false, 500, $"An error occurred in Creatint Member: {ex}", null);
            }
        }


        public async Task<ApiResponse<ViewMemberDto>> GetMember(int Id)
        {
            try
            {
                var member = await _context.Members.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

                if (member == null)
                {
                    return new ApiResponse<ViewMemberDto>(false, 404, "Member not found", null);
                }

                var memberDto = Mapper.MemberToMemberDto(member);

                return new ApiResponse<ViewMemberDto>(true, 200, "Member retrived succssefuly", memberDto);

            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred in Creating Member: {ex}");

                // Return ApiResponse with a 500 status code and an error message
                return new ApiResponse<ViewMemberDto>(false, 500, $"An error occurred in retriving Member: {ex}", null);
            }

        }

        public async Task<ApiResponse<PaginatedList<ViewMemberDto>>> GetPersons(string searchInput, MemberType memberType, int page, int pageSize)
        {

            try
            {
                var members = _context.Members.AsQueryable();

                if (!string.IsNullOrEmpty(searchInput))
                {
                    members = members.Where(x => x.NameEn.Contains(searchInput) || x.NameAr.Contains(searchInput));
                }

                //var personsCount = await players.Where(x => !x.IsDeleted).CountAsync();

                var membersToReturn = await members
                    .Where(x => !x.IsDeleted && x.Type == memberType)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(member => Mapper.MemberToMemberDto(member))
                    .ToListAsync();

                var paginatedList = new PaginatedList<ViewMemberDto>(membersToReturn, membersToReturn.Count(), page, pageSize);

                return new ApiResponse<PaginatedList<ViewMemberDto>>(true, 200, "Members retrived succssefuly", paginatedList);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred in Get Members: {ex}");

                return new ApiResponse<PaginatedList<ViewMemberDto>>(false, 404, "Failed to retrived Members Data", null);
            }

        }

        public async Task<ApiResponse<ViewMemberDto>> UpdateMember(UpdateMemberDto input)
        {
            try
            {
                var member = await _context.Members.FirstOrDefaultAsync(x => x.Id == input.Id && !x.IsDeleted);

                if (member == null)
                {
                    return new ApiResponse<ViewMemberDto>(false, 404, "Member not found", null);
                }

                // Delete the old image after checking if the user is found
                ImageProcess.DeleteImage(member.ImageUrl);

                // Update user information
                member.Type = input.PersonType;
                member.NameAr = input.NameAr;
                member.NameEn = input.NameEn;
                member.DescriptionAr = input.DescriptionAr;
                member.DescriptionEn = input.DescriptionEn;
                member.DateOfBirth = input.DateOfBirth;
                member.ImageUrl = ImageProcess.UploadImage(input.Image);
                member.UpdatedAt = DateTime.Now;

                // Save changes to the database

                _context.Members.Update(member);
                await _context.SaveChangesAsync();

                // Return ApiResponse with a success status code and updated player information
                var updatedMemberDto = Mapper.MemberToMemberDto(member);
                return new ApiResponse<ViewMemberDto>(true, 200, "Member updated successfully", updatedMemberDto);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred in Update Member: {ex}");

                // Return ApiResponse with a 500 status code and an error message
                return new ApiResponse<ViewMemberDto>(false, 500, $"An error occurred in Update Member: {ex}", null);
            }
        }

        public async Task<ApiResponse<ViewMemberDto>> DeleteMember(int Id)
        {
            try
            {
                var member = await _context.Members.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

                if (member == null)
                {
                    return new ApiResponse<ViewMemberDto>(false, 404, "Member not found", null);
                }

                member.IsActive = false;
                member.IsDeleted = true;

                _context.Members.Update(member);

                await _context.SaveChangesAsync();

                var memberDto = Mapper.MemberToMemberDto(member);

                return new ApiResponse<ViewMemberDto>(true, 200, "Person deleted successfully", memberDto);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred in Delete Member: {ex}");

                // Return ApiResponse with a 500 status code and an error message
                return new ApiResponse<ViewMemberDto>(false, 500, $"An error occurred in Delete Member: {ex}", null);
            }
        }

        public async Task<ApiResponse<ViewMemberDto>> UpdateMemberStatus(int Id)
        {
            try
            {
                var member = await _context.Members.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

                if (member == null)
                {
                    return new ApiResponse<ViewMemberDto>(false, 404, "Member Status Updated successfully", null);
                }

                member.IsActive = !member.IsActive;

                member.UpdatedAt = DateTime.Now;

                _context.Members.Update(member);

                await _context.SaveChangesAsync();

                var personDto = Mapper.MemberToMemberDto(member);

                return new ApiResponse<ViewMemberDto>(true, 200, "Member Status updated successfully", personDto);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred in Changing Member Status: {ex}");

                // Return ApiResponse with a 500 status code and an error message
                return new ApiResponse<ViewMemberDto>(false, 500, $"An error occurred in updating Member Status : {ex}", null);
            }
        }

    }
}
