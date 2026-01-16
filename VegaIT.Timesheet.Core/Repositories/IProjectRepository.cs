using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegaIT.Timesheet.Core.Entities;

namespace VegaIT.Timesheet.Core.Repositories;

public interface IProjectRepository
{
    void Create(Project project);
    PageOf<Project> FindPageByFirstLetterAndNameSubstring(string firstLetter, String name, int pageSize, int pageNumber);
    void Edit(Project project); //Da li edit treba da bude void?
    void DeleteById(Guid id);
    Maybe<Project> FindById(string id);
}
