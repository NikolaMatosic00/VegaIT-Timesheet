using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VegaIT.Timesheet.Core.Entities.Validations.ClientValidators
{
    public class ClientCountry
    {
        private readonly string _value;

        private ClientCountry(string value)
        {
            _value = value;
        }

        public static Result<ClientCountry> Create(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
                return Result.Failure<ClientCountry>("Conutry field can't be empty");

            if (country.Length > 100)
                return Result.Failure<ClientCountry>("Country name is too long");



            return Result.Success(new ClientCountry(country));
        }

        public static implicit operator string(ClientCountry country)
        {
            return country._value;
        }

        public override bool Equals(object obj)
        {
            ClientCountry country = obj as ClientCountry;

            if (ReferenceEquals(country, null))
                return false;

            return _value == country._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
