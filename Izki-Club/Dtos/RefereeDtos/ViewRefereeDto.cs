namespace Izki_Club.Dtos.RefereeDtos
{
    public class ViewRefereeDto
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int Age { get; set; }
        public string Image {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
