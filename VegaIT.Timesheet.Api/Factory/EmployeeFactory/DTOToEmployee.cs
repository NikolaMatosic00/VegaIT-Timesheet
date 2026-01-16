using CSharpFunctionalExtensions;
using VegaIT.Timesheet.Api.DTOs;
using VegaIT.Timesheet.Core.Entities;
using VegaIT.Timesheet.Core.Entities.Enumerations;

namespace VegaIT.Timesheet.Api.Factory.EmployeeFactory
{
    public class DTOToEmployee
    {
        public static Result<Employee> ConvertDTO(EmployeeDTO dto)
        {
            EStatusOfEmployee statusOfEmployee = (EStatusOfEmployee)Enum.Parse(typeof(EStatusOfEmployee), dto.Status, true);
            ERole employeeRole = (ERole)Enum.Parse(typeof(ERole), dto.Role, true);

            var result = Employee.Create(Guid.Parse(dto.EmployeeId), dto.Name, dto.Username, dto.Password,
                dto.Email, dto.HoursPerWeek, statusOfEmployee, employeeRole);

            return result;
        }
    }
}
