using Microsoft.AspNetCore.Mvc;
using VegaIT.Timesheet.Api.DTOs;
using VegaIT.Timesheet.Api.Factory.CategoryFactory;
using VegaIT.Timesheet.Api.Factory.ProjectFactory;
using VegaIT.Timesheet.Core.Entities;
using VegaIT.Timesheet.Core.Repositories;

namespace VegaIT.Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepo;

        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectRepo = projectRepository;
        }

        [HttpGet]
        public ActionResult<PageOf<Project>> GetPage(PageRequestDTO pageRequest)
        {
            return Ok(_projectRepo.FindPageByFirstLetterAndNameSubstring(
                pageRequest.FirstLetter, pageRequest.Name,
                pageRequest.pageSize, pageRequest.PageNumber));
        }

        [HttpPost]
        public ActionResult Create(ProjectDTO dto)
        {
            bool isValid = Guid.TryParse(dto.ProjectId, out _);
            if (!isValid)
            {
                return BadRequest("Provided ID is not in form of guid");
            }
            var result = DTOToProject.ConvertDTO(dto);
            if (result.IsSuccess)
            {
                _projectRepo.Create(result.Value);
                return Ok();
            }

            return BadRequest(result.Error);
        }

        [HttpPut]
        public ActionResult Put(ProjectDTO dto)
        {
            bool isValid = Guid.TryParse(dto.ProjectId, out _);
            if (!isValid)
            {
                return BadRequest("Provided ID is not in form of guid");
            }
            var result = DTOToProject.ConvertDTO(dto);
            if (result.IsSuccess)
            {
                _projectRepo.Edit(result.Value);
                return Ok();
            }

            return BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            bool isValid = Guid.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest("Provided ID is not in form of guid");
            }
            _projectRepo.DeleteById(Guid.Parse(id));

            return NoContent();
        }

    }
}
