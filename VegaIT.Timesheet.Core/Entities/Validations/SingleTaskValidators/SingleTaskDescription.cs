using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VegaIT.Timesheet.Core.Entities.Validations.SingleTaskValidators
{
    public class SingleTaskDescription
    {
        private readonly string _value;

        private SingleTaskDescription(string value)
        {
            _value = value;
        }

        public static Result<SingleTaskDescription> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<SingleTaskDescription>("Description can't be empty");

            if (description.Length > 300)
                return Result.Failure<SingleTaskDescription>("Description is too long");

            return Result.Success(new SingleTaskDescription(description));
        }

        public static implicit operator string(SingleTaskDescription description)
        {
            return description._value;
        }

        public override bool Equals(object obj)
        {
            SingleTaskDescription description = obj as SingleTaskDescription;

            if (ReferenceEquals(description, null))
                return false;

            return _value == description._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
