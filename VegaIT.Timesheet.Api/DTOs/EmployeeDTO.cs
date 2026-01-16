namespace VegaIT.Timesheet.Api.DTOs
{
    public class EmployeeDTO
    {
        public string EmployeeId { get; set; }

        public String Name { get; set; }

        public String Username { get; set; }

        public String Password { get; set; }

        public String Email { get; set; }

        public float HoursPerWeek { get; set; }

        public string Status { get; set; }

        public string Role { get; set; }
    }
}
