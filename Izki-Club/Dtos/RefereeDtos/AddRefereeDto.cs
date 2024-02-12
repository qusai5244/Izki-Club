using static Izki_Club.Enums.Member.MemberTypeEnum;
using System.ComponentModel.DataAnnotations;
using Izki_Club.Dtos.baseDtos;

namespace Izki_Club.Dtos.RefereeDtos
{
    public class AddRefereeDto : BaseDto
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

    }
}
