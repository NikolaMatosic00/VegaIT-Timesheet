using CSharpFunctionalExtensions;
using System.Net;
using System.Xml.Linq;
using VegaIT.Timesheet.Core.Entities.Validations.ClientValidators;
using VegaIT.Timesheet.Core.Entities.Validations.SingleTaskValidators;

namespace VegaIT.Timesheet.Core.Entities;


public class SingleTask
{

    public Guid Id { get; }

    public Client Client { get; }

    public Category Category { get; }

    public Project Project { get; }

    public DateOnly Date { get; }

    public String Description { get; }

    public float WorkingHours { get; }

    public float OvertimeHours { get; }

    public static Result<SingleTask> Create(Guid id, Client client,
        Category category, Project project, string date, string description,
        float workingHours, float overtimeHours)
    {
        var dateResult = SingleTaskDate.Create(date);
        var desriptionResult = SingleTaskDescription.Create(description);
        var workingHoursResult = SingleTaskWorkingHours.Create(workingHours);
        var overtimeHoursResult = SingleTaskOvertimeHours.Create(overtimeHours);

        var result = Result.Combine(dateResult, desriptionResult, workingHoursResult, overtimeHoursResult);

        return result.IsSuccess
        ? Result.Success(new SingleTask(id, client, category, project, DateOnly.Parse(date), description, workingHours, overtimeHours))
        : Result.Failure<SingleTask>(result.Error);
    }

    private SingleTask(Guid id, Client client,
        Category category, Project project, DateOnly date, string desctiption, float workingHours, float overtimeHours)
    {
        Id = id;
        Client = client;
        Category = category;
        Project = project;
        Date = date;
        Description = desctiption;
        WorkingHours = workingHours;
        OvertimeHours = overtimeHours;
    }

}
