using System.ComponentModel.DataAnnotations;

namespace Izki_Club.Dtos.RefereeDtos
{
    public class UpdateRefereeDto : AddRefereeDto
    {

        [Required]
        public bool IsActive { get; set; }
    }
}
