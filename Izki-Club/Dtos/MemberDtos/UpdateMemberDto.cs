using Izki_Club.Dtos.baseDtos;
using System.ComponentModel.DataAnnotations;
using static Izki_Club.Helpers.Enum;

namespace Izki_Club.Dtos.PlayerDtos
{
    public class UpdateMemberDto : PersonDto
    {
        [Required]
        public MemberType PersonType { get; set; }
        [Required]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public IFormFile Image { get; set; }
    }
}
