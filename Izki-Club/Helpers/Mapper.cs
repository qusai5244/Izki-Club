//using Izki_Club.Dtos.CoachDtos;
using Izki_Club.Dtos.PlayerDtos;
using Izki_Club.Dtos.TeamDtos;
using Izki_Club.Models;
using System;

namespace Izki_Club.Helpers
{
    public class Mapper
    {

        public static ViewMemberDto MemberToMemberDto(Member input)
        {
            return new ViewMemberDto
            {
                Id = input.Id,
                MemberType = input.MemberType.ToString(),
                NameEn = input.NameEn,
                NameAr = input.NameAr,
                DescriptionEn = input.DescriptionEn,
                DescriptionAr = input.DescriptionAr,
                Image = input.ImageUrl,
                IsActive = input.IsActive,
                Age = CalculateAge(input.DateOfBirth),
                TeamId = input.TeamId,
                CreatedAt = input.CreatedAt,
                UpdatedAt = input.UpdatedAt,
            };
        }

        public static Member MemberDtoToMember(AddMemberDto input)
        {
            return new Member
            {
                MemberType = input.MemberType,
                DateOfBirth = input.DateOfBirth,
                NameEn = input.NameEn,
                NameAr = input.NameAr,
                DescriptionEn = input.DescriptionEn,
                DescriptionAr = input.DescriptionAr,
                ImageUrl = ImageProcess.UploadImage(input.Image),
                IsActive = true,
                CreatedAt = DateTime.Now,
                TeamId = input.TeamId,
            };
        }

        public static ViewTeamDto TeamToTeamDto(Team input)
        {
            return new ViewTeamDto
            {
                Id = input.Id,
                NameEn = input.NameEn,
                NameAr = input.NameAr,
                DescriptionEn = input.DescriptionEn,
                DescriptionAr = input.DescriptionAr,
                IsActive = input.IsActive,
                FoundDate = input.FoundDate,
                CreatedAt = input.CreatedAt,
                UpdatedAt = input.UpdatedAt,
                ImagePath = input.ImageUrl,
            };
        }

        public static Team TeamDtoToTeam(AddTeamDto input)
        {
            return new Team
            {
                NameEn = input.NameEn,
                NameAr = input.NameAr,
                DescriptionEn = input.DescriptionEn,
                DescriptionAr = input.DescriptionAr,
                IsActive = true,
                FoundDate = input.FoundDate,
                CreatedAt = DateTime.Now,
                ImageUrl = ImageProcess.UploadImage(input.Image),
            };
        }

        private static int CalculateAge(DateTime dateOfBirth)
        {
            DateTime currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateOfBirth.Year;

            // Adjust age if the birthday hasn't occurred yet this year
            if (currentDate.Month < dateOfBirth.Month || (currentDate.Month == dateOfBirth.Month && currentDate.Day < dateOfBirth.Day))
            {
                age--;
            }

            return age;
        }
    }
}
