using Izki_Club.Dtos.baseDtos;
using System.ComponentModel.DataAnnotations;

namespace Izki_Club.Dtos.TeamDtos
{
    public class AddTeamDto : BaseDto
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FoundDate { get; set; }
        public string ImageUrl { get; set; }

        [Required]
        public int OrganizationId { get; set; }
    }
}
