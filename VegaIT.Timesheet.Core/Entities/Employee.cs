using CSharpFunctionalExtensions;
using VegaIT.Timesheet.Core.Entities.Enumerations;
using VegaIT.Timesheet.Core.Entities.Validations.EmployeeValidators;

namespace VegaIT.Timesheet.Core.Entities;

public class Employee
{

    public Guid EmployeeId { get; }

    public String Name { get; }

    public String Username { get; }

    public String Password { get; }

    public String Email { get; }

    public float HoursPerWeek { get; }

    public EStatusOfEmployee Status { get; }

    public ERole Role { get; }


    public static Result<Employee> Create(Guid id, string name, string username,
        string password, string email, float hoursPerWeek, EStatusOfEmployee status, ERole role)
    {

        var nameResult = EmployeeName.Create(name);
        var usernameResult = EmployeeUsername.Create(username);
        var passwordResult = EmployeePassword.Create(password);
        var emailResult = EmployeeEmail.Create(email);
        var hoursPerWeekResult = EmployeeHoursPerWeek.Create(hoursPerWeek);

        var result = Result.Combine(nameResult, usernameResult, passwordResult, emailResult, hoursPerWeekResult);

        return result.IsSuccess
            ? Result.Success(new Employee(id, name, username, password, email, hoursPerWeek, status, role))
            : Result.Failure<Employee>(result.Error);
    }

    private Employee(Guid id, string name, string username, string password, string email, float hoursPerWeek, EStatusOfEmployee status, ERole role)
    {
        EmployeeId = id;
        Name = name;
        Username = username;
        Password = password;
        Email = email;
        HoursPerWeek = hoursPerWeek;
        Status = status;
        Role = role;
    }

}
