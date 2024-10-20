using Izki_Club.Dtos.GeneralDtos;
using Izki_Club.Models;
using static Izki_Club.Enums.Member.MemberTypeEnum;
using MemberType = Izki_Club.Models.MemberType;

namespace Izki_Club.Dtos.MemberDtos
{
    public class ViewMembersByType : SearchAndPaginationDto
    {
        public MemberType memberType { get; set; }
    }
}
