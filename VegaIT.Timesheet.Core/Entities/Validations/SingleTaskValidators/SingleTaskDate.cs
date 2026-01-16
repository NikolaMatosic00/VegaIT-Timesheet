using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VegaIT.Timesheet.Core.Entities.Validations.SingleTaskValidators
{
    public class SingleTaskDate
    {
        private readonly string _value;

        private SingleTaskDate(string value)
        {
            _value = value;
        }

        public static Result<SingleTaskDate> Create(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return Result.Failure<SingleTaskDate>("Date can't be empty");

            DateTime temp;
            if (!DateTime.TryParse(date, out temp))
                return Result.Failure<SingleTaskDate>("Date must be in right format");


            return Result.Success(new SingleTaskDate(date));
        }

        public static implicit operator string(SingleTaskDate date)
        {
            return date._value;
        }

        public override bool Equals(object obj)
        {
            SingleTaskDate date = obj as SingleTaskDate;

            if (ReferenceEquals(date, null))
                return false;

            return _value == date._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
