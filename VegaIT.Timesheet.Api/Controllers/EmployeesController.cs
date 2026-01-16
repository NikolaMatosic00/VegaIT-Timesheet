using Microsoft.AspNetCore.Mvc;
using VegaIT.Timesheet.Api.DTOs;
using VegaIT.Timesheet.Api.Factory.CategoryFactory;
using VegaIT.Timesheet.Api.Factory.EmployeeFactory;
using VegaIT.Timesheet.Core.Entities;
using VegaIT.Timesheet.Core.Repositories;
//galerija
namespace VegaIT.Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepo = employeeRepository;
        }

        [HttpGet]
        public ActionResult<PageOf<EmployeeDTO>> GetPage([FromQuery] PageRequestDTO parameters)
        {
            List<Employee> listOfEntities = (List<Employee>)_employeeRepo.FindPage(parameters.pageSize, parameters.PageNumber).Items;

            int totalPagesCount = _employeeRepo.FindPage(parameters.pageSize, parameters.PageNumber).TotalPagesCount;
            

            List<EmployeeDTO> listOfDTOs = new List<EmployeeDTO>();

            foreach(Employee e in listOfEntities){
                EmployeeDTO emp = EmployeeToDTO.ConvertEntity(e);
                listOfDTOs.Add(emp);
            }



            return Ok(new PageOf<EmployeeDTO>(totalPagesCount, listOfDTOs));
        }

        [HttpPost]
        public ActionResult Create(EmployeeDTO dto)
        {
            bool isValid = Guid.TryParse(dto.EmployeeId, out _);
            if (!isValid)
            {
                return BadRequest("Provided ID is not in form of guid");
            }
            if (_employeeRepo.FindById(dto.EmployeeId.ToString()).HasValue)
            {
                return BadRequest("Provided Id already exists in Database.");
            }

            var result = DTOToEmployee.ConvertDTO(dto);
            if (result.IsSuccess)
            {
                _employeeRepo.Create(result.Value);
                return Ok();
            }

            return BadRequest(result.Error);
        }

        [HttpPut]
        public ActionResult Put(EmployeeDTO dto)
        {
            bool isValid = Guid.TryParse(dto.EmployeeId, out _);
            if (!isValid)
            {
                return BadRequest("Provided ID is not in form of guid");
            }

            if (_employeeRepo.FindById(dto.EmployeeId.ToString()).HasValue)
            {
                var result = DTOToEmployee.ConvertDTO(dto);
                if (result.IsSuccess)
                {
                    _employeeRepo.Edit(result.Value);
                    return Ok();
                }
                return BadRequest("Nije uspelo menjenje");
            }
                return BadRequest("Provided Id does not exist in Database.");

        }


        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            bool isValid = Guid.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest("Provided ID is not in form of guid");
            }
            if (_employeeRepo.FindById(id).HasNoValue)
            {
                return BadRequest("Provided Id does not exist in Database.");
            }
            _employeeRepo.DeleteById(Guid.Parse(id));

            return NoContent();
        }

        [HttpPut("changepassword")]
        public ActionResult ChangePassword(ChangePasswordDTO chpaDTO)
        {
            bool isValid = Guid.TryParse(chpaDTO.Id, out _);
            if (!isValid)
            {
                return BadRequest("Provided ID is not in form of guid");
            }
            if (_employeeRepo.FindById(chpaDTO.Id).HasNoValue)
            {
                return BadRequest("Provided Id does not exist in Database.");
            }
            _employeeRepo.EditPassword(Guid.Parse(chpaDTO.Id), chpaDTO.NewPassword);
            return Ok();
        }

        [HttpGet("all")]
        [Produces("application/json")]
        public ActionResult<List<Employee>> FindALl()
        {
            return _employeeRepo.FindAll();
        }

    }
}
