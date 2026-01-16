using CSharpFunctionalExtensions;
using VegaIT.Timesheet.Core.Entities.Validations.SingleTaskValidators;
using VegaIT.Timesheet.Core.Entities.Validations.WorkDayValidators;

namespace VegaIT.Timesheet.Core.Entities;

public class WorkDay
{
    public Guid DayId { get; }

    public Employee Employee { get; }

    public DateOnly Date { get; }

    public List<SingleTask> Tasks { get; }

    public static Result<WorkDay> Create(Guid id, Employee employee, string workDayDate, List<SingleTask> tasks)
    {
        var workDayDateResult = SingleTaskDate.Create(workDayDate);

        var result = Result.Combine(workDayDateResult);

        return result.IsSuccess
        ? Result.Success(new WorkDay(id, employee, DateOnly.Parse(workDayDate), tasks))
        : Result.Failure<WorkDay>(result.Error);
    }

    private WorkDay(Guid id, Employee employee, DateOnly date, List<SingleTask> tasks)
    {
        DayId = id;
        Employee = employee;
        Date = date;
        Tasks = tasks;
    }

}
