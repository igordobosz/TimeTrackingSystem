﻿using System;
using System.Collections.Generic;
using System.Text;
using TimeTrackingSystem.Common.DTO;
using TimeTrackingSystem.Common.Services;
using TimeTrackingSystem.Common.ViewModels;
using TimeTrackingSystem.Data.Models;

namespace TimeTrackingSystem.Common.Contracts
{
    public interface IEmployeeService : IServiceBase<EmployeeViewModel>
    {
        Employee GetEmployeeByIdentityCode(string identityCode);
        bool IsEmployeeInWork(int id);
        WorkRegisterEvent FindLastWorkRegister(int id);
        bool RegisterEventIn(int employeeId, int endpointId);
        RegisterEventOutResponse RegisterEventOut(int employeeId, int endpointId);
        List<EmployeeViewModel> FilterEmployeeAutoComplete(string filter);
    }
}
