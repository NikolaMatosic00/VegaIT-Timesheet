using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace VegaIT.Timesheet.Core.Entities.Validations.CategoryValidators

{
    public class CategoryNameV
    {
        private readonly string _value;

        private CategoryNameV(string value)
        {
            _value = value;
        }

        public static Result<CategoryNameV> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<CategoryNameV>("category name can't be empty");

            if (name.Length > 100)
                return Result.Failure<CategoryNameV>("Category name is too long");



            return Result.Success(new CategoryNameV(name));
        }

        public static implicit operator string(CategoryNameV name)
        {
            return name._value;
        }

        public override bool Equals(object obj)
        {
            CategoryNameV name = obj as CategoryNameV;

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
