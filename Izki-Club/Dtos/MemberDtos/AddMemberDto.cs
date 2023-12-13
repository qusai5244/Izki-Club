using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Izki_Club.Dtos.baseDtos;
using static Izki_Club.Helpers.Enum;

namespace Izki_Club.Dtos.PlayerDtos
{
    public class AddMemberDto : BaseDto
    {
        [Required]
        public MemberType MemberType { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

        [Required]
        public int TeamId { get; set; }
    }
}
