using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Izki_Club.Dtos.baseDtos;
using static Izki_Club.Helpers.Enum;

namespace Izki_Club.Dtos.PlayerDtos
{
    public class ViewPersonDto : PersonDto
    {
        public PersonType PersonType { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Image { get; set; }
    }
}
