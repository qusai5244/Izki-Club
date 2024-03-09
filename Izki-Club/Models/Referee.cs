namespace Izki_Club.Models
{
    public class Referee : Person
    {
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}
