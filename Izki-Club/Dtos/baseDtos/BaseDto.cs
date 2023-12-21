using System.ComponentModel.DataAnnotations;
using static Izki_Club.Enums.Member.MemberTypeEnum;

namespace Izki_Club.Dtos.baseDtos
{
    public class BaseDto // this is for Coach, Player Models (all of them have the same properties)
    {

        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Only English characters are allowed.")]
        public string NameEn { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[؀-ۿ ]+$", ErrorMessage = "Only Arabic characters are allowed.")]
        public string NameAr { get; set; }

        [StringLength(500)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Only English characters are allowed.")]
        public string DescriptionEn { get; set; }

        [StringLength(500)]
        [RegularExpression("^[؀-ۿ ]+$", ErrorMessage = "Only Arabic characters are allowed.")]
        public string DescriptionAr { get; set; }
    }
}
