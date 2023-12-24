using Izki_Club.Dtos.GeneralDtos;
using Izki_Club.Models;
using static Izki_Club.Enums.Member.MemberTypeEnum;

namespace Izki_Club.Dtos.MemberDtos
{
    public class ViewMembersByType : SearchAndPaginationDto
    {
        public MemberType memberType { get; set; }
    }
}
