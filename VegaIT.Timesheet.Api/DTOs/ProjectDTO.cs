namespace VegaIT.Timesheet.Api.DTOs
{
    public class ProjectDTO
    {
        public string ProjectId { get; set; }

        public ClientDTO? Client { get; set; }

        public EmployeeDTO? Leader { get; set; }

        public String ProjectName { get; set; }

        public String Description { get; set; }

        public string Status { get; set; }
    }
}
