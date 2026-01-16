using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VegaIT.Timesheet.Core.Entities.Validations.ClientValidators
{
    public class ClientPostalCode
    {
        private readonly string _value;

        private ClientPostalCode(string value)
        {
            _value = value;
        }

        public static Result<ClientPostalCode> Create(string postalCode)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
                return Result.Failure<ClientPostalCode>("Postal code field can't be empty");

            if (postalCode.Length > 100)
                return Result.Failure<ClientPostalCode>("Postal code too long");



            return Result.Success(new ClientPostalCode(postalCode));
        }

        public static implicit operator string(ClientPostalCode postalCode)
        {
            return postalCode._value;
        }

        public override bool Equals(object obj)
        {
            ClientPostalCode name = obj as ClientPostalCode;

            if (ReferenceEquals(name, null))
                return false;

            return _value == name._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
