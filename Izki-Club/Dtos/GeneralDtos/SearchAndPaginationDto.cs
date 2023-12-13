namespace Izki_Club.Dtos.GeneralDtos
{
    public class SearchAndPaginationDto
    {
        public string SearchEn { get; set; }
        public string SearchAr { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
