using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegaIT.Timesheet.Core.Entities;

namespace VegaIT.Timesheet.Core.Repositories;

public interface IEmployeeRepository
{
    void Create(Employee employee);
    PageOf<Employee> FindPage(int pageSize, int pageNumber);
    void Edit(Employee employee);
    void EditPassword(Guid employeeId, String newPassword);
    void DeleteById(Guid id);
    List<Employee> FindAll(); //Za kreiranje novog projekta
    Maybe<Employee> FindById(string id);
}
