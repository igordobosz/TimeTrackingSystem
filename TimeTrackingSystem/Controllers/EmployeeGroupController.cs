﻿using System;
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
    [Route("api/EmployeeGroup")]
    public class EmployeeGroupController : CrudController<IEmployeeGroupService, EmployeeGroupViewModel>
    {
        public EmployeeGroupController(IEmployeeGroupService baseService) : base(baseService)
        {
        }

        [HttpGet]
        public List<EmployeeGroupCbxViewModel> ListCbx()
        {
            return _baseService.ListCbx();
        }
    }
}
