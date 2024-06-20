using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Izki_Club.Dtos.baseDtos;
using static Izki_Club.Enums.Member.MemberTypeEnum;

namespace Izki_Club.Dtos.PlayerDtos
{
    public class AddMemberDto
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
        [Required]
        public MemberType MemberType { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public string Image { get; set; }

        public int TeamId { get; set; }
    }
}
