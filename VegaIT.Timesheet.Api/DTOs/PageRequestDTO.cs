namespace VegaIT.Timesheet.Api.DTOs
{
    public class PageRequestDTO
    {

        public string? FirstLetter { get; set; }
        public string? Name { get; set; }
        public int pageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
