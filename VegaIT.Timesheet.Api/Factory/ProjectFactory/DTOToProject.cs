using CSharpFunctionalExtensions;
using VegaIT.Timesheet.Api.DTOs;
using VegaIT.Timesheet.Core.Entities;
using VegaIT.Timesheet.Core.Entities.Enumerations;

namespace VegaIT.Timesheet.Api.Factory.ProjectFactory
{
    public class DTOToProject
    {
        public static Result<Project> ConvertDTO(ProjectDTO dto)
        {
            var client = Client.Create(Guid.Parse(dto.Client.ClientId), dto.Client.Name, dto.Client.Address, dto.Client.City, dto.Client.PostalCode, dto.Client.Country);
            
            Enum.TryParse(dto.Leader.Status, out EStatusOfEmployee statusOfEmployee);
            Enum.TryParse(dto.Leader.Role, out ERole employeeRole);
            var employee = Employee.Create(Guid.Parse(dto.Leader.EmployeeId), dto.Leader.Name, dto.Leader.Username, dto.Leader.Password,
                dto.Leader.Email, dto.Leader.HoursPerWeek, statusOfEmployee, employeeRole);

            Enum.TryParse(dto.Status, out EStatusOfProject statusOfProject);
            var result = Project.Create(Guid.Parse(dto.ProjectId), client.Value, employee.Value, dto.ProjectName, dto.Description, statusOfProject);

            return result;
        }
    }
}
