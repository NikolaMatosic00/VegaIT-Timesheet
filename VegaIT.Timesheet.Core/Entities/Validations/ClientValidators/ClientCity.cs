using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VegaIT.Timesheet.Core.Entities.Validations.ClientValidators
{
    public class ClientCity
    {
        private readonly string _value;

        private ClientCity(string value)
        {
            _value = value;
        }

        public static Result<ClientCity> Create(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return Result.Failure<ClientCity>("City field can't be empty");

            if (city.Length > 150)
                return Result.Failure<ClientCity>("City name is too long");



            return Result.Success(new ClientCity(city));
        }

        public static implicit operator string(ClientCity city)
        {
            return city._value;
        }

        public override bool Equals(object obj)
        {
            ClientCity city = obj as ClientCity;

            if (ReferenceEquals(city, null))
                return false;

            return _value == city._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
