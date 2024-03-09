using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Izki_Club.Dtos.baseDtos;
using static Izki_Club.Enums.Member.MemberTypeEnum;
using Izki_Club.Models;

namespace Izki_Club.Dtos.PlayerDtos
{
    public class ViewMemberDto : BaseDto
    {
        public string MemberType { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Image { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
