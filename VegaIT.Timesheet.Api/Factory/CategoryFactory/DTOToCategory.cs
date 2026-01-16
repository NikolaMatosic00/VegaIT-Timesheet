using CSharpFunctionalExtensions;
using VegaIT.Timesheet.Api.DTOs;
using VegaIT.Timesheet.Core.Entities;

namespace VegaIT.Timesheet.Api.Factory.CategoryFactory
{
    public class DTOToCategory
    {
        public static Result<Category> ConvertDTO(CategoryDTO dto)
        {
            var entity = Category.Create(Guid.Parse(dto.Id), dto.CategoryName);

            return entity;
        }
    }
}
