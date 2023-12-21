using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using static Izki_Club.Enums.Member.MemberTypeEnum;

namespace Izki_Club.Models
{
    public class Member : Person
    {
        [Required]
        public MemberType MemberType { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }

    }
}
