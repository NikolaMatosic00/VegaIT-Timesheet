using Microsoft.AspNetCore.Mvc;
using VegaIT.Timesheet.Api.DTOs;
using VegaIT.Timesheet.Api.Factory.ProjectFactory;
using VegaIT.Timesheet.Core.Entities;
using VegaIT.Timesheet.Core.Repositories;

namespace VegaIT.Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkdaysController : ControllerBase
    {
        private readonly IWorkDayRepository _workdayRepo;

        public WorkdaysController(IWorkDayRepository workdayRepository)
        {
            _workdayRepo = workdayRepository;
        }


    }
}
