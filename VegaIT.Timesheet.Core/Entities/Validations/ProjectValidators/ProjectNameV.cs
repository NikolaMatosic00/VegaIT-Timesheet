using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegaIT.Timesheet.Core.Entities.Validations.CategoryValidators;

namespace VegaIT.Timesheet.Core.Entities.Validations.ProjectValidators
{
    public class ProjectNameV
    {
        private readonly string _value;

        private ProjectNameV(string value)
        {
            _value = value;
        }

        public static Result<ProjectNameV> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<ProjectNameV>("Project name can't be empty");

            if (name.Length > 100)
                return Result.Failure<ProjectNameV>("Project name is too long");



            return Result.Success(new ProjectNameV(name));
        }

        public static implicit operator string(ProjectNameV name)
        {
            return name._value;
        }

        public override bool Equals(object obj)
        {
            ProjectNameV name = obj as ProjectNameV;

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
