namespace VegaIT.Timesheet.Persistence.Models;


public class SingleTask
{

    public Guid Id { get; set; }

    public Guid WorkDayId { get; set; }

    public Guid ClientId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid ProjectId { get; set; }

    public DateTime Date { get; set; }

    public String Description { get; set; }

    public float WorkingHours { get; set; }

    public float OvertimeHours { get; set; }




}
