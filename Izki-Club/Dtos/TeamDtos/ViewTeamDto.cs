using Izki_Club.Dtos.baseDtos;

namespace Izki_Club.Dtos.TeamDtos
{
    public class ViewTeamDto : BaseDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime FoundDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ImagePath { get; set; }
    }
}
