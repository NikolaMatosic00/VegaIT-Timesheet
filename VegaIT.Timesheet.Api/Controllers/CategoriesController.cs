using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VegaIT.Timesheet.Api.DTOs;
using VegaIT.Timesheet.Api.Factory.CategoryFactory;
using VegaIT.Timesheet.Core.Entities;
using VegaIT.Timesheet.Core.Repositories;
using VegaIT.Timesheet.Persistence.Models.DBContext;
using VegaIT.Timesheet.Persistence.Repositories;

// umesto pageDTO treba u url

namespace VegaIT.Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoriesController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public ActionResult<PageOf<CategoryDTO>> GetPage([FromQuery] PageRequestDTO parameters)
        {
            List<Category> listOfEntities = (List<Category>)_categoryRepo.FindPageByFirstLetterAndNameSubstring(
                parameters.FirstLetter, parameters.Name,parameters.pageSize, parameters.PageNumber).Items;

            int totalPagesCount = _categoryRepo.FindPageByFirstLetterAndNameSubstring(
                parameters.FirstLetter, parameters.Name, parameters.pageSize, parameters.PageNumber).TotalPagesCount;
            

            List<CategoryDTO> listOfDTOs = new List<CategoryDTO>();

            foreach(Category c in listOfEntities){
                CategoryDTO cate = CategoryToDTO.ConvertEntity(c);
                listOfDTOs.Add(cate);
            }
            return Ok(new PageOf<CategoryDTO>(totalPagesCount, listOfDTOs));
        }

        [HttpPost]
        public ActionResult Create(CategoryDTO dto)
        {
            bool isValid = Guid.TryParse(dto.Id, out _);
            if (!isValid)
            {
                return BadRequest("Provided ID is not in form of guid");
            }
            if (_categoryRepo.FindById(dto.Id).HasValue)
            {
                return BadRequest("Provided Id already exists in Database. :(");
            }

            var result = DTOToCategory.ConvertDTO(dto);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            _categoryRepo.Create(result.Value);
            return Ok();
        }

        [HttpPut]
        public ActionResult Put(CategoryDTO dto)
        {
            bool isValid = Guid.TryParse(dto.Id, out _);
            if (!isValid)
            {
                return BadRequest("Provided ID is not in form of guid");
            }
            if (_categoryRepo.FindById(dto.Id).HasNoValue)
            {
                return BadRequest("Provided Id does not exist in Database.");
            }

            var result = DTOToCategory.ConvertDTO(dto);
            if (result.IsSuccess)
            {
                _categoryRepo.Edit(result.Value);
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
            if (_categoryRepo.FindById(id).HasNoValue)
            {
                return BadRequest("Provided Id does not exist in Database.");
            }
            _categoryRepo.DeleteById(Guid.Parse(id));

            return NoContent();
        }

    }
}
