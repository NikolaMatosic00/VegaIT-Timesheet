using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegaIT.Timesheet.Core.Entities.Validations.EmployeeValidators;

namespace VegaIT.Timesheet.Core.Entities.Validations.SingleTaskValidators
{
    public class SingleTaskWorkingHours
    {
        private readonly float _value;

        private SingleTaskWorkingHours(float value)
        {
            _value = value;
        }

        public static Result<SingleTaskWorkingHours> Create(float workingHours)
        {

            float n;
            if (!float.TryParse(workingHours.ToString(), out n))
                return Result.Failure<SingleTaskWorkingHours>("Hours can not be text");

            if (!Enumerable.Range(0, 24).Contains(int.Parse(workingHours.ToString().Substring(0, 3))))
                return Result.Failure<SingleTaskWorkingHours>("Unreal working hours");

            if (!Enumerable.Range(0, 60).Contains(int.Parse(workingHours.ToString().Substring(3, 5))))
                return Result.Failure<SingleTaskWorkingHours>("Unreal minutes input");

            return Result.Success(new SingleTaskWorkingHours(workingHours));
        }

        public static implicit operator float(SingleTaskWorkingHours workingHours)
        {
            return workingHours._value;
        }

        public override bool Equals(object obj)
        {
            SingleTaskWorkingHours workingHours = obj as SingleTaskWorkingHours;

            if (ReferenceEquals(workingHours, null))
                return false;

            return _value == workingHours._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
