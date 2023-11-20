using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Izki_Club.Dtos.baseDtos;
using static Izki_Club.Helpers.Enum;

namespace Izki_Club.Dtos.PlayerDtos
{
    public class AddPersonDto : PersonDto
    {
        [Required]
        public PersonType PersonType { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
    }
}
