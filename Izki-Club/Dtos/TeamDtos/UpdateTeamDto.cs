using Izki_Club.Dtos.baseDtos;
using System.ComponentModel.DataAnnotations;

namespace Izki_Club.Dtos.TeamDtos
{
    public class UpdateTeamDto : BaseDto
    {

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FoundDate { get; set; }

        [Required]
        public bool IsActive { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
