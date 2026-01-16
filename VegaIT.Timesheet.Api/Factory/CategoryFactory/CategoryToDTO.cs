using VegaIT.Timesheet.Api.DTOs;
using VegaIT.Timesheet.Core.Entities;

namespace VegaIT.Timesheet.Api.Factory.CategoryFactory
{
    public class CategoryToDTO
    {
        public static CategoryDTO ConvertEntity(Category entity)
        {
            var dto = new CategoryDTO
            {
                Id = entity.CategoryId.ToString(),
                CategoryName = entity.CategoryName
            };
            return dto;
        }
    }
}
