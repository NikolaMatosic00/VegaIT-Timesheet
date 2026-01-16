using VegaIT.Timesheet.Api.DTOs;
using VegaIT.Timesheet.Core.Entities;

namespace VegaIT.Timesheet.Api.Factory.EmployeeFactory
{
    public class EmployeeToDTO
    {
        public static EmployeeDTO ConvertEntity(Employee entity)
        {
            var dto = new EmployeeDTO
            {
                EmployeeId = entity.EmployeeId.ToString(),
                Name = entity.Name,
                Username = entity.Username,
                Password = entity.Password,
                Email = entity.Email,
                HoursPerWeek = entity.HoursPerWeek,
                Status = entity.Status.ToString(),
                Role = entity.Role.ToString()

            };
            return dto;
        }
    }
}