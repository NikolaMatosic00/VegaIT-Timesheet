using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VegaIT.Timesheet.Core.Entities.Validations.ClientValidators
{
    public class ClientAddress
    {
        private readonly string _value;

        private ClientAddress(string value)
        {
            _value = value;
        }

        public static Result<ClientAddress> Create(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return Result.Failure<ClientAddress>("Address can't be empty");

            if (address.Length > 150)
                return Result.Failure<ClientAddress>("Address name is too long");



            return Result.Success(new ClientAddress(address));
        }

        public static implicit operator string(ClientAddress address)
        {
            return address._value;
        }

        public override bool Equals(object obj)
        {
            ClientAddress address = obj as ClientAddress;

            if (ReferenceEquals(address, null))
                return false;

            return _value == address._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
