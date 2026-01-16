using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegaIT.Timesheet.Core.Entities.Validations.SingleTaskValidators;

namespace VegaIT.Timesheet.Core.Entities.Validations.WorkDayValidators
{
    public class WorkDayDate
    {
        private readonly string _value;

        private WorkDayDate(string value)
        {
            _value = value;
        }

        public static Result<WorkDayDate> Create(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return Result.Failure<WorkDayDate>("Date can't be empty");

            DateTime temp;
            if (!DateTime.TryParse(date, out temp))
                return Result.Failure<WorkDayDate>("Date must be in right format");

            return Result.Success(new WorkDayDate(date));
        }

        public static implicit operator string(WorkDayDate date)
        {
            return date._value;
        }

        public override bool Equals(object obj)
        {
            WorkDayDate date = obj as WorkDayDate;

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

