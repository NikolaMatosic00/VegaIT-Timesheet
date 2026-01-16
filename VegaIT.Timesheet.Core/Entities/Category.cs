using CSharpFunctionalExtensions;
using VegaIT.Timesheet.Core.Entities.Validations.CategoryValidators;
using VegaIT.Timesheet.Core.Entities.Validations.ProjectValidators;

namespace VegaIT.Timesheet.Core.Entities;

public interface ICategory
{
    public Guid CategoryId { get; }

    public String CategoryName { get; }
}

// write model
public class Category : ICategory
{
    public Guid CategoryId { get; }

    public String CategoryName { get; }

    public static Result<Category> Create(Guid id, string name)
    {
        var nameResult = CategoryNameV.Create(name);

        return nameResult.IsSuccess
        ? Result.Success(new Category(id, name))
        : Result.Failure<Category>(nameResult.Error);
    }

    private Category(Guid id, string categoryName)
    {
        CategoryId = id;
        CategoryName = categoryName;
    }

}

// read model
public class CategoryRead : ICategory
{
    public Guid CategoryId { get; }

    public String CategoryName { get; }

    public CategoryRead(Guid id, string categoryName)
    {
        CategoryId = id;
        CategoryName = categoryName;
    }
}
