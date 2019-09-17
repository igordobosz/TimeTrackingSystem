using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingSystem.Common.Contracts;
using TimeTrackingSystem.Common.Services;
using TimeTrackingSystem.Common.ViewModels;
using TimeTrackingSystem.Data.Models;

namespace TimeTrackingSystem.Controllers
{
    [Route("api/Employee")]
    public class EmployeeController : CrudController<IEmployeeService, EmployeeViewModel>
    {
        public EmployeeController(IEmployeeService baseService) : base(baseService)
        {
        }
    }
}
