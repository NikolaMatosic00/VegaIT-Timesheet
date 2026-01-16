namespace VegaIT.Timesheet.Persistence.Models;

public class Employee
    {

	public Guid EmployeeId { get; set; }

    public String Name { get; set; }

    public String Username { get; set; }

    public String Password { get; set; }

    public String Email { get; set; }

    public float HoursPerWeek { get; set; }

    public String Status { get; set; }

    public String Role { get; set; }


}
