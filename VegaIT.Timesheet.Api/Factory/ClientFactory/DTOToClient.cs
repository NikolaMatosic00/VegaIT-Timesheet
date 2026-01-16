using VegaIT.Timesheet.Api.DTOs;
using VegaIT.Timesheet.Core.Entities;
using CSharpFunctionalExtensions;

namespace VegaIT.Timesheet.Api.Factory.CategoryFactory
{
    public class DTOToClient
    {
        public static Result<Client> ConvertDTO(ClientDTO dto)
        {
            var result = Client.Create(Guid.Parse(dto.ClientId), dto.Name, dto.Address, dto.City, dto.PostalCode, dto.Country);

            return result;
        }
    }
}
