using Microsoft.AspNetCore.Mvc;
using VegaIT.Timesheet.Api.DTOs;
using VegaIT.Timesheet.Api.Factory.CategoryFactory;
using VegaIT.Timesheet.Core.Entities;
using VegaIT.Timesheet.Core.Repositories;
using CSharpFunctionalExtensions;
using VegaIT.Timesheet.Api.Factory.ClientFactory;
using Microsoft.AspNetCore.Cors;

namespace VegaIT.Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("http://localhost:3000/")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepo;

        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepo = clientRepository;
        }

        [HttpGet]
        public ActionResult<PageOf<ClientDTO>> GetPage([FromQuery] PageRequestDTO parameters)
        {

            List<Client> listOfEntities = (List<Client>)_clientRepo.FindPageByFirstLetterAndNameSubstring(
                parameters.FirstLetter, parameters.Name,parameters.pageSize, parameters.PageNumber).Items;

            int totalPagesCount = _clientRepo.FindPageByFirstLetterAndNameSubstring(
                parameters.FirstLetter, parameters.Name, parameters.pageSize, parameters.PageNumber).TotalPagesCount;
            

            List<ClientDTO> listOfDTOs = new List<ClientDTO>();

            foreach(Client c in listOfEntities){
                ClientDTO cl = ClientToDTO.ConvertEntity(c);
                listOfDTOs.Add(cl);
            }



            return Ok(new PageOf<ClientDTO>(totalPagesCount, listOfDTOs));
        }

        [HttpPost]
        public ActionResult Create(ClientDTO dto)
        {
            Guid guidOutput;
            bool isValid = Guid.TryParse(dto.ClientId, out guidOutput);
            if (!isValid)
            {
                return BadRequest("Provided ID is not in form of guid");
            }
            if (_clientRepo.FindById(dto.ClientId.ToString()).HasValue)
            {
                return BadRequest("Provided Id already exists in Database. :(");
            }

            var result = DTOToClient.ConvertDTO(dto);
            if (result.IsSuccess)
            {
                _clientRepo.Create(result.Value);
                return Ok();
            }

            return BadRequest(result.Error);
        }

        [HttpPut]
        public ActionResult Put(ClientDTO dto)
        {
            bool isValid = Guid.TryParse(dto.ClientId, out _);
            if (!isValid)
            {
                return BadRequest("Provided ID is not in form of guid");
            }
            if (_clientRepo.FindById(dto.ClientId.ToString()).HasNoValue)
            {
                return BadRequest("Provided Id does not exist in Database.");
            }

            var result = DTOToClient.ConvertDTO(dto);
            if (result.IsSuccess)
            {
                _clientRepo.Edit(result.Value);
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
            if (_clientRepo.FindById(id).HasNoValue)
            {
                return BadRequest("Provided Id does not exist in Database.");
            }
            _clientRepo.DeleteById(Guid.Parse(id));

            return NoContent();
        }

        [HttpGet("all")]
        [Produces("application/json")]
        public ActionResult<List<Client>> FindALl()
        {
            return _clientRepo.FindAll();
        }

    }
}
