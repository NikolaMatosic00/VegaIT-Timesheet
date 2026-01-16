using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VegaIT.Timesheet.Core.Entities.Validations.SingleTaskValidators
{
    public class SingleTaskOvertimeHours
    {
        private readonly float _value;

        private SingleTaskOvertimeHours(float value)
        {
            _value = value;
        }

        public static Result<SingleTaskOvertimeHours> Create(float overtimeHours)
        {
            float n;
            if (!float.TryParse(overtimeHours.ToString(), out n))
                return Result.Failure<SingleTaskOvertimeHours>("Overtime hours can not be text");

            if (!Enumerable.Range(0, 24).Contains(int.Parse(overtimeHours.ToString().Substring(0, 3))))
                return Result.Failure<SingleTaskOvertimeHours>("Unreal overtime working hours");

            if (!Enumerable.Range(0, 60).Contains(int.Parse(overtimeHours.ToString().Substring(3, 5))))
                return Result.Failure<SingleTaskOvertimeHours>("Unreal overtime minutes input");


            return Result.Success(new SingleTaskOvertimeHours(overtimeHours));
        }

        public static implicit operator float(SingleTaskOvertimeHours overtimeHours)
        {
            return overtimeHours._value;
        }

        public override bool Equals(object obj)
        {
            SingleTaskOvertimeHours overtimeHours = obj as SingleTaskOvertimeHours;

            if (ReferenceEquals(overtimeHours, null))
                return false;

            return _value == overtimeHours._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
