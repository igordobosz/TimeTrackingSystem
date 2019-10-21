using System;
using System.Collections.Generic;
using System.Text;
using TimeTrackingSystem.Common.DTO;

namespace TimeTrackingSystem.Common.ViewModels
{
    public class RegisterTimePerEmployeeViewModel
    {
        public TimeSpan SummaryWorkTime { get; set; }
        public int WorkDays { get; set; }
        public TimeSpan OverTimes { get; set; }
        public FindByConditionResponse<RegisterTimePerEmployeeDayWrapperViewModel> WorkEventDayList { get; set; }
    }
}
