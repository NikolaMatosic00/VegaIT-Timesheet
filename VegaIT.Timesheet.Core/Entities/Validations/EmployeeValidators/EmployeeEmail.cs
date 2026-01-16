using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VegaIT.Timesheet.Core.Entities.Validations.EmployeeValidators
{
    public class EmployeeEmail
    {
        private readonly string _value;

        private EmployeeEmail(string value)
        {
            _value = value;
        }

        public static Result<EmployeeEmail> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure<EmployeeEmail>("Email field can't be empty");

            if (email.Length > 100)
                return Result.Failure<EmployeeEmail>("Email is too long");

            if(!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                return Result.Failure<EmployeeEmail>("E-mail is invalid");

            return Result.Success(new EmployeeEmail(email));
        }

        public static implicit operator string(EmployeeEmail email)
        {
            return email._value;
        }

        public override bool Equals(object obj)
        {
            EmployeeEmail email = obj as EmployeeEmail;

            if (ReferenceEquals(email, null))
                return false;

            return _value == email._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
