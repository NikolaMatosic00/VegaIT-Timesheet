using VegaIT.Timesheet.Api.DTOs;
using VegaIT.Timesheet.Core.Entities;

namespace VegaIT.Timesheet.Api.Factory.ClientFactory
{
    public class ClientToDTO
    {
        //dto.ClientId, dto.Name, dto.Address, dto.City, dto.PostalCode, dto.Country
        public static ClientDTO ConvertEntity(Client entity)
        {
            var dto = new ClientDTO
            {
                ClientId = entity.ClientId.ToString(),
                Name = entity.Name,
                Address = entity.Address,
                City = entity.City,
                PostalCode = entity.PostalCode,
                Country = entity.Country
            };
            return dto;
        }
    }
}
