using System;
using System.Collections.Generic;
using System.Text;
using TimeTrackingSystem.Common.DTO;
using TimeTrackingSystem.Common.Services;
using TimeTrackingSystem.Common.ViewModels;

namespace TimeTrackingSystem.Common.Contracts
{
    public interface IWorkRegisterEventService : IServiceBase<WorkRegisterEventViewModel>
    {
        RegisterTimePerEmployeeViewModel GetWorkEventsByEmployeeAndDate(int employeeId, DateTime date);
        RegisterTimePerDayViewModel GetWorkEventsByDay(DateTime date);
    }
}
