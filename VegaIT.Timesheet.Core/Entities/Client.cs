using CSharpFunctionalExtensions;
using System.Net;
using System.Xml.Linq;
using VegaIT.Timesheet.Core.Entities.Validations.ClientValidators;
using VegaIT.Timesheet.Core.Entities.Validations.ProjectValidators;
using VegaIT.Timesheet.Core.Entities.Validations.SingleTaskValidators;

namespace VegaIT.Timesheet.Core.Entities;


public class Client
{

    public Guid ClientId { get; }

    public String Name { get; }

    public String Address { get; }

    public String City { get; }

    public String PostalCode { get; }

    public String Country { get; }

    public static Result<Client> Create(Guid id, string name, string address,
        string city, string clientPostalCode, string clientCountry)
    {

        var nameResult = ClientName.Create(name);
        var addressResult = ClientAddress.Create(address);
        var cityResult = ClientCity.Create(city);
        var clientPostalCodeResult = ClientPostalCode.Create(clientPostalCode);
        var clientCountryResult = ClientCountry.Create(clientCountry);

        var result = Result.Combine(nameResult, addressResult, cityResult, clientPostalCodeResult, clientCountryResult);

        return result.IsSuccess
        ? Result.Success(new Client(id, name, address, city, clientPostalCode, clientCountry))
        : Result.Failure<Client>(result.Error);

    }

    private Client(Guid id, string name, string address, string city, string clientPostalCode, string clientCountry)
    {
        ClientId = id;
        Name = name;
        Address = address;
        City = city;
        PostalCode = clientPostalCode;
        Country = clientCountry;
    }

}

