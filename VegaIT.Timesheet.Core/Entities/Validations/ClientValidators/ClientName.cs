using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegaIT.Timesheet.Core.Entities.Validations.CategoryValidators;

namespace VegaIT.Timesheet.Core.Entities.Validations.ClientValidators
{
    public class ClientName
    {
        private readonly string _value;

        private ClientName(string value)
        {
            _value = value;
        }

        public static Result<ClientName> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<ClientName>("Name can't be empty");

            if (name.Length > 100)
                return Result.Failure<ClientName>("Client name is too long");



            return Result.Success(new ClientName(name));
        }

        public static implicit operator string(ClientName name)
        {
            return name._value;
        }

        public override bool Equals(object obj)
        {
            ClientName name = obj as ClientName;

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
