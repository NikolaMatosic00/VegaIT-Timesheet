namespace VegaIT.Timesheet.Persistence.Models;

public class WorkDay

{
    public Guid WorkDayId { get; set; }

    public Guid EmployeeId { get; set; }

    public DateTime Date { get; set; }

}
