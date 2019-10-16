using System;
using System.Collections.Generic;
using System.Text;
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
        bool RegisterEventOut(int employeeId, int endpointId);
    }
}
