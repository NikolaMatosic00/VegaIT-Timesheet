using VegaIT.Timesheet.Api.DTOs;
using VegaIT.Timesheet.Api.Factory.ClientFactory;
using VegaIT.Timesheet.Api.Factory.EmployeeFactory;
using VegaIT.Timesheet.Core.Entities;

namespace VegaIT.Timesheet.Api.Factory.ProjectFactory
{
    public class ProjectToDTO
    {
        public static ProjectDTO ConvertEntity(Project entity)
        {
            var dto = new ProjectDTO
            {
                ProjectId = entity.ProjectId.ToString(),
                Client = ClientToDTO.ConvertEntity(entity.Client),
                Leader = EmployeeToDTO.ConvertEntity(entity.Leader),
                ProjectName = entity.ProjectName,
                Description = entity.Description,
                Status = entity.Status.ToString()
            };
            return dto;
        }
    }
}