using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VegaIT.Timesheet.Core.Entities.Validations.EmployeeValidators
{
    public class EmployeeUsername
    {
        private readonly string _value;

        private EmployeeUsername(string value)
        {
            _value = value;
        }

        public static Result<EmployeeUsername> Create(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return Result.Failure<EmployeeUsername>("Username can't be empty");

            if (username.Length > 120)
                return Result.Failure<EmployeeUsername>("username is too long");



            return Result.Success(new EmployeeUsername(username));
        }

        public static implicit operator string(EmployeeUsername username)
        {
            return username._value;
        }

        public override bool Equals(object obj)
        {
            EmployeeUsername username = obj as EmployeeUsername;

            if (ReferenceEquals(username, null))
                return false;

            return _value == username._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
