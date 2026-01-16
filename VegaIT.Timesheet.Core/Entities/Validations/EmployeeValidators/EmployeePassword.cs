using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VegaIT.Timesheet.Core.Entities.Validations.EmployeeValidators
{
    public class EmployeePassword
    {
        private readonly string _value;

        private EmployeePassword(string value)
        {
            _value = value;
        }

        public static Result<EmployeePassword> Create(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return Result.Failure<EmployeePassword>("Password can't be empty");

            if (password.Length < 7 || password.Length > 180)
                return Result.Failure<EmployeePassword>("Password must be between 7 and 140 characters long");



            return Result.Success(new EmployeePassword(password));
        }

        public static implicit operator string(EmployeePassword password)
        {
            return password._value;
        }

        public override bool Equals(object obj)
        {
            EmployeePassword password = obj as EmployeePassword;

            if (ReferenceEquals(password, null))
                return false;

            return _value == password._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
