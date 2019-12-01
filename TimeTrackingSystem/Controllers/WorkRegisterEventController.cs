using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingSystem.Common.Contracts;
using TimeTrackingSystem.Common.DTO;
using TimeTrackingSystem.Common.Services;
using TimeTrackingSystem.Common.ViewModels;
using TimeTrackingSystem.Data.Models;

namespace TimeTrackingSystem.Controllers
{
    [Route("api/WorkRegisterEvent")]
    public class WorkRegisterEventController : CrudController<IWorkRegisterEventService, WorkRegisterEventViewModel>
    {
        public WorkRegisterEventController(IWorkRegisterEventService baseService) : base(baseService)
        {
        }

        [HttpGet]
        [Route("GetWorkEventsByEmployeeAndDate")]
        public RegisterTimePerEmployeeViewModel GetWorkEventsByEmployeeAndDate(int employeeID, DateTime date, bool IsSumOvertimes, int tolerance)
        {
            return _baseService.GetWorkEventsByEmployeeAndDate(employeeID, date, IsSumOvertimes, tolerance);
        }

        [HttpGet]
        [Route("GetWorkEventsByDay")]
        public RegisterTimePerDayViewModel GetWorkEventsByDay(DateTime date, int tolerance)
        {
            return _baseService.GetWorkEventsByDay(date, tolerance);
        }

        [HttpGet]
        [Route("GetEmployeesInWork")]
        public RegisterTimePerDayViewModel GetEmployeesInWork()
        {
            return _baseService.GetEmployeesInWork();
        }
    }
}
