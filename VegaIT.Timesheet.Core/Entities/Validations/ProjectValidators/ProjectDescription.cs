using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VegaIT.Timesheet.Core.Entities.Validations.ProjectValidators
{
    public class ProjectDescription
    {
        private readonly string _value;

        private ProjectDescription(string value)
        {
            _value = value;
        }

        public static Result<ProjectDescription> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<ProjectDescription>("Project name can't be empty");

            if (description.Length > 100)
                return Result.Failure<ProjectDescription>("Project name is too long");



            return Result.Success(new ProjectDescription(description));
        }

        public static implicit operator string(ProjectDescription description)
        {
            return description._value;
        }

        public override bool Equals(object obj)
        {
            ProjectDescription description = obj as ProjectDescription;

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
