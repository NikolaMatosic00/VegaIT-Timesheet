using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegaIT.Timesheet.Core.Entities.Validations.SingleTaskValidators;

namespace VegaIT.Timesheet.Core.Entities.Validations.EmployeeValidators
{
    public class EmployeeHoursPerWeek
    {
        private readonly float _value;

        private EmployeeHoursPerWeek(float value)
        {
            _value = value;
        }

        public static Result<EmployeeHoursPerWeek> Create(float hoursPerWeek)
        {
            var val = hoursPerWeek - Math.Truncate(hoursPerWeek);
            float n;
            if (!float.TryParse(hoursPerWeek.ToString(), out n))
                return Result.Failure<EmployeeHoursPerWeek>("Hours per week can not be text");

            if (Int32.Parse(hoursPerWeek.ToString().Substring(0, 2)) > 168 || hoursPerWeek < 0)
                return Result.Failure<EmployeeHoursPerWeek>("Unreal hours per week input");

            if (val > 59 || val < 0)
                return Result.Failure<EmployeeHoursPerWeek>("Unreal minutes input");


            return Result.Success(new EmployeeHoursPerWeek(hoursPerWeek));
        }

        public static implicit operator float(EmployeeHoursPerWeek hoursPerWeek)
        {
            return hoursPerWeek._value;
        }

        public override bool Equals(object obj)
        {
            EmployeeHoursPerWeek hoursPerWeek = obj as EmployeeHoursPerWeek;

            if (ReferenceEquals(hoursPerWeek, null))
                return false;

            return _value == hoursPerWeek._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
