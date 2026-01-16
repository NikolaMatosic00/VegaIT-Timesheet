using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegaIT.Timesheet.Core.Entities;

namespace VegaIT.Timesheet.Core.Repositories;

public interface ICategoryRepository
{
    void Create(Category category);
    //pageSize * pageNumber = od kog mu iz baze vracam, rezultat + pageSize = do kog mu vracam. Sve ih po povratku smestam u listu koja se nalazi u Page Generic Klasi koja mi svaki put(da li treba ovako?) broji koliko ih ima da bi znao koliko stranica da ponudim
    PageOf<Category> FindPageByFirstLetterAndNameSubstring(string firstLetter, String name, int pageSize, int pageNumber);
    void Edit(Category category);
    void DeleteById(Guid id);
    Maybe<Category> FindById(string id);
}
