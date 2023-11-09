using System.ComponentModel.DataAnnotations;

namespace Izki_Club.Models
{
    public class GeneralModelBase
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string NameEn { get; set; }
        [Required]
        public string NameAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
