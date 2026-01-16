using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegaIT.Timesheet.Core.Entities;

namespace VegaIT.Timesheet.Core.Repositories;

public interface IWorkDayRepository
{
    void Create(WorkDay workDay);
    List<WorkDay> FindWorkingMonthByEmployeeIdAndDate(Guid employeeId, DateOnly date); //Za trenutni mesec dohvata workDays 7 dana pre i 7 dana posle
    List<WorkDay> FindWorkingWeekByEmployeeIdAndDate(Guid employeeId, DateOnly date); //dohvata workDays za odabranu nedelju, da li je bolje int?
    Maybe<WorkDay> FindByEmployeeIdAndDate(Guid employeeId, DateOnly date);
}
