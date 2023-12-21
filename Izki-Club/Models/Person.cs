using static Izki_Club.Enums.Member.MemberTypeEnum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Izki_Club.Models
{
    public class Person
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(255)")]
        public string NameEn { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(255)")]
        public string NameAr { get; set; }

        [StringLength(500)] // Set the maximum length as needed
        [Column(TypeName = "varchar(500)")] // Set the database column type
        public string DescriptionEn { get; set; }

        [StringLength(500)] // Set the maximum length as needed
        [Column(TypeName = "nvarchar(500)")] // Set the database column type for Unicode (Arabic)
        public string DescriptionAr { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
