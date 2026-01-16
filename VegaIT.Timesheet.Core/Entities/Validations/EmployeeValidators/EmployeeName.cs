using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegaIT.Timesheet.Core.Entities.Validations.CategoryValidators;

namespace VegaIT.Timesheet.Core.Entities.Validations.EmployeeValidators
{
    public class EmployeeName
    {
        private readonly string _value;

        private EmployeeName(string value)
        {
            _value = value;
        }

        public static Result<EmployeeName> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<EmployeeName>("Name can't be empty");

            if (name.Length > 100)
                return Result.Failure<EmployeeName>("Name is too long");



            return Result.Success(new EmployeeName(name));
        }

        public static implicit operator string(EmployeeName name)
        {
            return name._value;
        }

        public override bool Equals(object obj)
        {
            EmployeeName name = obj as EmployeeName;

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
