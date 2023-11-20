//using Izki_Club.Dtos.CoachDtos;
using Izki_Club.Dtos.PlayerDtos;
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
                PersonType = input.Type,
                NameEn = input.NameEn,
                NameAr = input.NameAr,
                DescriptionEn = input.DescriptionEn,
                DescriptionAr = input.DescriptionAr,
                Image = input.ImageUrl,
                IsActive = input.IsActive,
                Age = CalculateAge(input.DateOfBirth),
                CreatedAt = input.CreatedAt,
                UpdatedAt = input.UpdatedAt,
            };
        }

        public static Member MemberDtoToMember(AddMemberDto input)
        {
            return new Member
            {
                Type = input.PersonType,
                DateOfBirth = input.DateOfBirth,
                NameEn = input.NameEn,
                NameAr = input.NameAr,
                DescriptionEn = input.DescriptionEn,
                DescriptionAr = input.DescriptionAr,
                ImageUrl = ImageProcess.UploadImage(input.Image),
                IsActive = true,
                CreatedAt = DateTime.Now
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
